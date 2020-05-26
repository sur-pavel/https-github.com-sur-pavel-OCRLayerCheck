using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCRLayerCheck;

namespace ArticleParserTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ArticleTest()
        {
            Patterns patterns = new Patterns();
            Log log = new Log();
            log.CreateLogFile();

            ArticleParser articleParser = new ArticleParser(log, patterns);
            Article actualArticle = new Article();
            StringBuilder stringBuilder = new StringBuilder(
                "Bulletin de correspondance hellenique moderne et contemporain 1 | 2019 " +
                "Le mariage dans le pourtour mediterraneen de l’Europe " +
                "- Controverse The Byzantines between Civil and Sacramental Marriage " +
                "Les Byzantins entre mariage civil et mariage religieux " +
                "Katerina Nikolaou " +
                "Electronic version URL: http://journals.openedition.org/bchmc/285 Publisher Ecole francaise d'Athenes " +
                "Electronic reference " +
                "Katerina Nikolaou, " +
                "« The Byzantines between Civil and Sacramental Marriage », " +
                "Bulletin de correspondance hellenique moderne et contemporain[Online], " +
                "1 | 2019, Online since 01 December 2019, connection on 20 December 2019." +
                "URL : http://journals.openedition.org/bchmc/285 " +
                "Bulletin de correspondance hellenique moderne et contemporain hellenique moderne et contemporain");
            actualArticle.PdfText = stringBuilder;
            actualArticle = articleParser.ParsePdfText(actualArticle);
            Article expectedArticle = new Article();
            expectedArticle.PdfText = stringBuilder;
            expectedArticle.Autor = "Katerina Nikolaou";
            expectedArticle.Title = "The Byzantines between Civil and Sacramental Marriage";
            expectedArticle.Year = "2019";
            expectedArticle.Journal.Title = "BCHMC";
            expectedArticle.Journal.Number = "2019";
            expectedArticle.Journal.Volume = "1";
            AssertAll(expectedArticle, actualArticle);
        }

        [TestMethod]
        public void BookTest()
        {
            Patterns patterns = new Patterns();
            Log log = new Log();
            ArticleParser articleParser = new ArticleParser(log, patterns);
            Article actualArticle = new Article();
            StringBuilder stringBuilder = new StringBuilder(
                "Qu'est-ce qu'un systeme philosophique ? Cours 2007 et 2008 Jacques Bouveresse DOI: " +
                "10.4000 / books.cdf.1715 Editeur: College de France Annee d'edition : 2012 Date de mise " +
                "en ligne: 4 avril 2013 Collection: Philosophie de la connaissance ISBN electronique: " +
                "9782722601529 http://books.openedition.org " +
                "Reference electronique " +
                "BOUVERESSE, Jacques.Qu'est-ce qu'un systeme philosophique ? Cours 2007 et 2008." +
                "Nouvelle edition [en ligne].Paris : College de France, 2012" +
                "(genere le 02 mai 201 9).Disponible sur Internet : <http://books.openedition.org/cdf/1715>." +
                "ISBN : 9782722601529.DOI : 10.4000 / books.cdf.1715. Ce document a ete genere automatiquement le 2 mai 2019. " +
                "© College de France, 2012 Conditions d’utilisation: http://www.openedition.org/6540");
            actualArticle.PdfText = stringBuilder;
            actualArticle = articleParser.ParsePdfText(actualArticle);
            Article expectedArticle = new Article();
            expectedArticle.PdfText = stringBuilder;
            expectedArticle.Autor = "Bouveresse Jacques";
            expectedArticle.Title = "Qu'est-ce qu'un systeme philosophique?";
            expectedArticle.Town = "Paris";
            expectedArticle.Year = "2012";

            AssertAll(expectedArticle, actualArticle);
        }

        private void AssertAll(Article expectedArticle, Article actualArticle)
        {
            Assert.AreEqual(expectedArticle.Autor, actualArticle.Autor);
            Assert.AreEqual(expectedArticle.Title, actualArticle.Title);
            Assert.AreEqual(expectedArticle.Town, actualArticle.Town);
            Assert.AreEqual(expectedArticle.Year, actualArticle.Year);
            Assert.AreEqual(expectedArticle.Journal.Title, actualArticle.Journal.Title);
            Assert.AreEqual(expectedArticle.Journal.Number, actualArticle.Journal.Number);
            Assert.AreEqual(expectedArticle.Journal.Volume, actualArticle.Journal.Volume);
        }
    }
}