using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class WordWrapHelper
{
    private static readonly string _punctuation = ".,。、！!？?〉》」』｣)）]】〕〗〙〛}>)❩❫❭❯❱❳❵｝";

    public static IEnumerable<string> WordWrap(string text, double wrapWidth, double widthOfASpace, Func<string, double> measure, bool allowBreakingWordsApart)
    {
        var curLine = 0;
        var atStartOfLine = true;
        var x = 0.0;
        var wordPieces = new List<string>();
        var wordPiecesWidths = new List<double>();
        var wordPiecesWidthsSum = 0.0;
        var actualWidth = 0.0;
        var numSpaces = 0;

        var sb = new StringBuilder();

        Action renderSpaces = new Action(() =>
        {
            sb.Append(' ', numSpaces);
            x += numSpaces * widthOfASpace;
            actualWidth = Math.Max(actualWidth, x);
            numSpaces = 0;
        });

        Action renderPieces = new Action(() =>
        {
            // Add a space if we are not at the beginning of the line.
            if (!atStartOfLine)
                renderSpaces();
            for (int j = 0; j < wordPieces.Count; j++)
                sb.Append(wordPieces[j]);
            x += wordPiecesWidthsSum;
            actualWidth = Math.Max(actualWidth, x);
            wordPieces.Clear();
            wordPiecesWidths.Clear();
            wordPiecesWidthsSum = 0;
        });

        // The parameter is not used, but it may be useful in future
        Func<bool, string> advanceToNextLine = new Func<bool, string>((bool newParagraph) =>
        {
            var line = sb.ToString();
            sb = new StringBuilder();
            x = 0;
            atStartOfLine = true;
            curLine++;
            numSpaces = 0;
            return line;
        });

        var i = 0;
        while (i < text.Length)
        {
            // Check whether we are at the start of a word, and if so, how long the word is.
            int lengthOfWord = 0;

            while (lengthOfWord + i < text.Length && !isWrappableAfter(text, lengthOfWord + i) && text[lengthOfWord + i] != '\n')
                lengthOfWord++;

            // If the word is followed by a punctuation mark, don’t wrap it

            while (lengthOfWord + i < text.Length && _punctuation.Contains(text[lengthOfWord + i]))
                lengthOfWord++;
            if (lengthOfWord > 0)
            {
                // We are looking at a word. (It doesn’t matter whether we’re at the beginning of the word or in the middle of one.)
                retry1:
                string fragment = text.Substring(i, lengthOfWord);
                var fragmentWidth = measure(fragment);
                retry2:

                // If we are at the start of a line, and the word itself doesn’t fit on a line by itself, give up
                if (atStartOfLine && x + wordPiecesWidthsSum + fragmentWidth > wrapWidth)
                {
                    if (!allowBreakingWordsApart)
                    {
                        // Return null to signal that we encountered a word that doesn’t fit in a line.
                        yield return null;
                        yield break;
                    }

                    // We don’t know exactly where to break the word, so use binary search to discover where that is.
                    if (lengthOfWord > 1)
                    {
                        lengthOfWord /= 2;
                        goto retry1;
                    }

                    // If we get to here, ‘WordPieces’ contains as much of the word as fits into one line, and the next letter makes it too long.
                    // If ‘WordPieces’ is empty, we are at the beginning of a paragraph and the first letter already doesn’t fit.
                    if (wordPieces.Count > 0)
                    {
                        // Render the part of the word that fits on the line and then move to the next line.
                        renderPieces();
                        yield return advanceToNextLine(false);
                    }
                }
                else if (!atStartOfLine && x + numSpaces * widthOfASpace + wordPiecesWidthsSum + fragmentWidth > wrapWidth)
                {
                    // We have already rendered some text on this line, but the word we’re looking at right now doesn’t
                    // fit into the rest of the line, so leave the rest of this line blank and advance to the next line.
                    yield return advanceToNextLine(false);

                    // In case the word also doesn’t fit on a line all by itself, go back to top (now that ‘AtStartOfLine’ is true)
                    // where it will check whether we need to break the word apart.
                    goto retry2;
                }

                // If we get to here, the current fragment fits on the current line (or it is a single character that overflows
                // the line all by itself).
                wordPieces.Add(fragment);
                wordPiecesWidths.Add(fragmentWidth);
                wordPiecesWidthsSum += fragmentWidth;
                i += lengthOfWord;
            }

            // We encounter the end of a word. All the word pieces fit on the current line, so render them.
            if (wordPieces.Count > 0)
            {
                renderPieces();
                atStartOfLine = false;
            }

            if (i < text.Length && text[i] == '\n')
            {
                // If the whitespace character is actually a newline, start a new paragraph.
                yield return advanceToNextLine(true);
                i++;
            }
            else if (i < text.Length && char.IsWhiteSpace(text, i))
            {
                // Discover the extent of the spaces.
                numSpaces = 0;
                while (numSpaces + i < text.Length && isWrappableAfter(text, numSpaces + i) && text[numSpaces + i] != '\n')
                    numSpaces++;
                i += numSpaces;

                if (atStartOfLine)
                {
                    // If we are at the beginning of the line, treat these spaces as the paragraph’s indentation.
                    renderSpaces();
                }
            }
        }

        renderPieces();
        if (sb.Length > 0)
            yield return sb.ToString();
    }

    private static bool isWrappableAfter(string txt, int index)
    {
        switch (txt[index])
        {
            // Return false for all the whitespace characters that should NOT be wrappable
            // NO-BREAK SPACE and NARROW NO-BREAK SPACE
            case '\u00a0':
            case '\u202f':
                return false;
            // Return true for all the NON-whitespace characters that SHOULD be wrappable
            // ZERO WIDTH SPACE
            case '\u200b':
                return true;
            // Apart from the above exceptions, wrap at whitespace characters.
            default:
                return char.IsWhiteSpace(txt, index);
        }
    }

    public static void SetWordWrappedText(ref string text, TextMesh tm, Renderer tr, Transform ownerTransform)
    {
        tm.gameObject.SetActive(true);
        tm.lineSpacing = 1f;

        var low = 1;
        var high = 256;
        var wrappeds = new Dictionary<int, string>();
        var origText = tm.text;
        var origRotation = tm.transform.rotation;
        tm.transform.eulerAngles = new Vector3(90, 0, 0);

        var desiredWidth = 0.115f * ownerTransform.lossyScale.x;
        var desiredHeight = 0.075f * ownerTransform.lossyScale.x;
        while (high - low > 1)
        {
            var mid = (low + high) / 2;
            tm.fontSize = mid;

            tm.text = "\u00a0";
            var size = tr.bounds.size;
            var widthOfASpace = size.x;

            var wrappedSB = new StringBuilder();
            var first = true;
            foreach (var line in WordWrap(
                text,
                desiredWidth,
                widthOfASpace,
                str =>
                {
                    tm.text = str;
                    return tr.bounds.size.x;
                },
                allowBreakingWordsApart: false
            ))
            {
                if (line == null)
                {
                    // There was a word that was too long to fit into a line.
                    high = mid;
                    wrappedSB = null;
                    break;
                }
                if (!first)
                    wrappedSB.Append('\n');
                first = false;
                wrappedSB.Append(line);
            }

            if (wrappedSB != null)
            {
                var wrapped = wrappedSB.ToString();
                wrappeds[mid] = wrapped;
                tm.text = wrapped;
                size = tr.bounds.size;
                if (size.z > desiredHeight)
                    high = mid;
                else
                    low = mid;
            }
        }
        text = wrappeds[low];
        tm.text = text;
        tm.fontSize = low;
        tm.transform.rotation = origRotation;
        tm.gameObject.SetActive(true);
    }
}
