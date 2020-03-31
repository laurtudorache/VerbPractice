using NUnit.Framework;
using VerbsPracticeApp.Models;

namespace WebPracticeApp.UnitTests.Models
{
    [TestFixture]
    public class TenseModelTests
    {
        private readonly string referenceExpected = "xxx";

        [Test]
        public void CreateInstance_WhenNullAnswer_IsReplacedWithEmptyString()
        {
            var sut = new TenseModel(referenceExpected, null);
            Assert.IsNotNull(sut.Answer);
            Assert.IsTrue(sut.Answer.Length == 0);
        }

        [Test]
        public void CreateInstance_WhenEmptyAnswer_AnswerIsNotChanged()
        {
            var sut = new TenseModel(referenceExpected, string.Empty);
            Assert.IsNotNull(sut.Answer);
            Assert.IsTrue(sut.Answer.Length == 0);
        }

        [Test]
        public void CreateInstance_WhenSpacesAsAnswer_AnswerIsEmptyString()
        {
            var sut = new TenseModel(referenceExpected, "      ");
            Assert.IsNotNull(sut.Answer);
            Assert.IsTrue(sut.Answer.Length == 0);
        }

        [Test]
        public void CreateInstance_WhenSpacesAroundAnswer_AnswerIsTrimmed()
        {
            var sut = new TenseModel(referenceExpected, $"  {referenceExpected}  ");
            Assert.IsTrue(sut.Answer.Equals(referenceExpected));
            Assert.IsTrue(sut.IsAnswerCorrect);
        }

        [Test]
        public void CreateInstance_WhenIsAnswer_AnswerIslowerCase()
        {
            var sut = new TenseModel(referenceExpected, referenceExpected.ToUpper());
            Assert.IsTrue(sut.Answer.Equals(referenceExpected));
            Assert.IsTrue(sut.IsAnswerCorrect);
        }

        [Test]
        public void CreateInstance_WhenIsAnswerTheSameAsReference_AnswerIsCorrect()
        {
            var sut = new TenseModel(referenceExpected, referenceExpected);
            Assert.IsTrue(sut.IsAnswerCorrect);
        }

        [Test]
        public void CreateInstance_WhenIsNullReference_ReferenceIslowerCase()
        {
            var sut = new TenseModel(null, string.Empty);
            Assert.IsNotNull(sut.Reference);
            Assert.IsTrue(sut.Reference.Length == 0);
        }

        [Test]
        public void CreateInstance_WhenIsNullReferenceAndNullAnswer_TenseIsCorrect()
        {
            var sut = new TenseModel(null, null);
            Assert.IsTrue(sut.IsAnswerCorrect);
        }
    }
}