using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AutoFixture;
using NSubstitute;
using Moq;
using BLL.Services;
using DAL.Interfaces;
using DAL.Entities.SeekerResumeBilder;
using BLL.DTO.SeekerResumeBuilder;
using BLL.Infrastructure;

namespace BLL.UnitTests
{
    [TestFixture]
    public class ResumeServiceTests
    {
        private readonly IFixture _fixture = new Fixture();

        private ResumeService _service;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        protected void SetUp()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _fixture.Inject(_unitOfWork);

            _service = _fixture.Create<ResumeService>();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                                    .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public void Create_resume_writes_no_null()
        {
            //Arrange
            SeekerResumeDTO seekerResumeDTO = _fixture.Create<SeekerResumeDTO>();

            //Act
            _service.Create(seekerResumeDTO);

            //Assert
            _unitOfWork.SeekerResumes.Received().Create(Arg.Is<SeekerResume>(p => p.VacanciesAcceptedBy != null
                                                 && p.SkillSets != null));
        }

        [Test]
        public void Delete_should_call_repository_delete_once()
        {
            // Arrange
            SeekerResume resume = _fixture.Create<SeekerResume>();
            int Id = _fixture.Create<int>();
            _unitOfWork.SeekerResumes.Get(Id).Returns(resume);

            // Act
            _service.Delete(Id);

            // Assert
            _unitOfWork.SeekerResumes.Received(1).Delete(Id);
        }

        [Test]
        public void Delete_should_call_repository_Exeption()
        {
            try
            {
                // Arrange
                SeekerResume resume = _fixture.Create<SeekerResume>();
                int Id = _fixture.Create<int>();
                //_unitOfWork.SeekerResumes.Get(Id).Returns(resume);

                // Act
                _service.Delete(Id);

                // Assert
                _unitOfWork.SeekerResumes.Received(1).Delete(Id);
            }
            catch (ValidationException ex)
            {
                Assert.AreEqual("Resume does not exist", ex.Message);
            }

        }

        [Test]
        public void Finde_key_should_return_resum_no_null()
        {
            //Arrange
            SeekerResume seekerResume = _fixture.Create<SeekerResume>();
            string key = _fixture.Create<string>();
            seekerResume.FirstName = key;
            _unitOfWork.SeekerResumes.GetAll().Returns(new[] { seekerResume });

            //Act
            IEnumerable<SeekerResumeDTO> tResums = _service.Find(key);

            //Assert
            Assert.That(tResums.Count() == 1);


        }

        [Test]
        public void Finde_key_should_return_resum_Exeption()
        {
            try
            {
                //Arrange
                SeekerResume seekerResume = _fixture.Create<SeekerResume>();
                string key = _fixture.Create<string>();
                seekerResume.FirstName = key;
                //_unitOfWork.SeekerResumes.GetAll().Returns(new[] { seekerResume });

                //Act
                IEnumerable<SeekerResumeDTO> tResums = _service.Find(key);


            }
            catch (ValidationException ex)
            {
                Assert.AreEqual("No matches", ex.Message);
            }
        }

        [Test]
        public void GetAll_should_return_resume_no_null()
        {

            //Arrange
            SeekerResume seekeResume = _fixture.Create<SeekerResume>();


            _unitOfWork.SeekerResumes.GetAll().Returns(new[] { seekeResume });

            //Act
            IEnumerable<SeekerResumeDTO> tPosts = _service.GetAll();

            //Assert
            Assert.That(tPosts.Count() == 1);

        }


        [Test]
        public void GetAll_should_return_resume_Exeption()
        {
            try
            {

                //Arrange
                SeekerResume seekeResume = _fixture.Create<SeekerResume>();

                //Act
                IEnumerable<SeekerResumeDTO> tPosts = _service.GetAll();

                //Assert
                Assert.That(tPosts.Count() == 1);
            }
            catch (ValidationException ex)
            {
                Assert.AreEqual("No resumes yet", ex.Message);
            }

        }

        [Test]
        public void Get_should_return_resume_no_null()
        {

            //Arrange
            SeekerResume seekerResume = _fixture.Create<SeekerResume>();
            int resumeID = _fixture.Create<int>();
            seekerResume.Id = resumeID;

            _unitOfWork.SeekerResumes.Get(resumeID).Returns(seekerResume);

            //Act
            SeekerResumeDTO tSeekerResume = _service.Get(resumeID);

            //Assert
            Assert.That(tSeekerResume != null);

        }


        [Test]
        public void Get_should_return_resume_Exeption()
        {
            try
            {
                //Arrange
                SeekerResume seekerResume = _fixture.Create<SeekerResume>();
                int resumeID = _fixture.Create<int>();
                seekerResume.Id = resumeID;

                _unitOfWork.SeekerResumes.Get(resumeID).Returns(seekerResume);

                //Act
                SeekerResumeDTO tSeekerResume = _service.Get(resumeID);

                //Assert
                Assert.That(tSeekerResume != null);
            }
            catch (ValidationException ex)
            {
                Assert.AreEqual("Resume not found", ex.Message);
            }

        }




    }


}


