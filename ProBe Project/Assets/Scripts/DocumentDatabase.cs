using System;

public static class DocumentDatabase
{
    // Array of references to all the documents.
    public static Document[] documentDatabase;

    private static float currentScore;
    private static char[] spliters = new char[3];

    // Sets the static docuemnt list to the list of docuements in the active scene.
    public static void AddDocumentsAtStart(Document[] documents)
    {
        documentDatabase = documents;
        foreach (Document doc in documentDatabase)
        {
            doc.searchScore = 0f;
        }
        spliters[0] = ' ';
        spliters[1] = '"';
        spliters[2] = '.';
    }

    // Searches all of the documents in the list.
    public static void PerformSearch(string input)
    {
        string[] inputArray = input.Split(spliters);
        foreach (Document doc in documentDatabase)
        {
            foreach(string word in inputArray)
            {
                // Checks the input against the tag.
                foreach (string tag in doc.docTags)
                {
                    if (word.Equals(tag))
                        currentScore += 1f;
                }

                // Checks the input against the contents.
                string[] docStrings = doc.content.Split(spliters);
                foreach (string docWord in docStrings)
                {
                    if (word.ToLower().Equals(docWord.ToLower()))
                        currentScore += 0.5f;
                }
            }
            doc.searchScore = currentScore;
            currentScore = 0f;
        }

        // Sorts them in order of from highest score to lowest score.
        Array.Sort(documentDatabase, CompareDocumentsByScores);
    }

    // Compares two documents by their search score.
    private static int CompareDocumentsByScores(Document doc1, Document doc2)
    {
        if (doc1.searchScore == doc2.searchScore)
            return 0;
        else if (doc1.searchScore < doc2.searchScore)
            return 1;
        else
            return -1;
    }
}