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
using BLL.DTO.JobPostManagement;
using DAL.Interfaces;
using DAL.Entities.JobPostManagement;
using BLL.Infrastructure;
using DAL.Entities.SeekerResumeBilder;
using BLL.DTO.SeekerResumeBuilder;

namespace BLL.UnitTests
{
    [TestFixture]
    public class JobServiceTests
    {
        private readonly IFixture _fixture = new Fixture();

        private JobService _service;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        protected void SetUp()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _fixture.Inject(_unitOfWork);

            _service = _fixture.Create<JobService>();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                                    .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }


        //Checks if create method passes correct object to uow repo
        [Test]
        public void Create_jobpost_writes_no_null()
        {
            //Arrange
            JobPostDTO jobPostDTO = _fixture.Create<JobPostDTO>();

            //Act
            _service.Create(jobPostDTO);

            //Assert
            _unitOfWork.JobPosts.Received().Create(Arg.Is<JobPost>(p => p.SubmitedResumes != null
                                                 && p.SkillSets != null
                                                 && p.JobLocation != null));
        }

        [Test]
        public void Delete_should_call_repository_delete_once()
        {
            // Arrange

            JobPost jobPost = _fixture.Create<JobPost>();
            int Id = _fixture.Create<int>();
            _unitOfWork.JobPosts.Get(Id).Returns(jobPost);

            // Act
            _service.Delete(Id);

            // Assert
            _unitOfWork.JobPosts.Received(1).Delete(Id);
        }

        [Test]
        public void Delete_should_call_repository_Exeption()
        {
            try
            {
                // Arrange


                JobPost jobPost = _fixture.Create<JobPost>();
                int Id = _fixture.Create<int>();
                //_unitOfWork.JobPosts.Get(Id).Returns(jobPost);

                // Act
                _service.Delete(Id);

                // Assert
                _unitOfWork.JobPosts.Received(1).Delete(Id);
            }

            catch (ValidationException ex)
            {
                Assert.AreEqual("Job post doesn not exist", ex.Message);
            }

        }

        [Test]
        public void Find_returns_value()
        {
            // Arrange
            /*
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                                    .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());*/

            JobPost jobPost = _fixture.Create<JobPost>();
            string key = _fixture.Create<string>();
            jobPost.CompanyName = key;
            _unitOfWork.JobPosts.GetAll().Returns(new[] { jobPost });

            // Act
            IEnumerable<JobPostDTO> tPosts = _service.Find(key);

            // Assert
            Assert.That(tPosts.Count() == 1);
        }

        [Test]
        public void Find_returns_Exeption()
        {
            try
            {
                //Arrange
                JobPost jobPost = _fixture.Create<JobPost>();
                string key = _fixture.Create<string>();
                jobPost.CompanyName = key;
                //_unitOfWork.JobPosts.GetAll().Returns(new[] { jobPost });

                //Act
                IEnumerable<JobPostDTO> tPosts = _service.Find(key);

                //Assert
                Assert.That(tPosts.Count() == 1);
            }
            catch (ValidationException ex)
            {
                Assert.AreEqual("No matches", ex.Message);
            }


        }



        [Test]
        public void GetAll_should_return_job_no_null()
        {

            //Arrange
            JobPost jobPost = _fixture.Create<JobPost>();


            _unitOfWork.JobPosts.GetAll().Returns(new[] { jobPost });

            //Act
            IEnumerable<JobPostDTO> tPosts = _service.GetAll();

            //Assert
            Assert.That(tPosts.Count() == 1);

        }

        [Test]
        public void GetAll_should_return_job_Exeption()
        {
            try
            {
                //Arrange
                JobPost jobPost = _fixture.Create<JobPost>();


                //Act
                IEnumerable<JobPostDTO> tPosts = _service.GetAll();

                //Assert
                Assert.That(tPosts.Count() == 1);
            }
            catch (ValidationException ex)
            {
                Assert.AreEqual("No job posts yet", ex.Message);
            }

        }
        [Test]
        public void Get_should_return_job_no_null()
        {

            //Arrange
            JobPost jobPost = _fixture.Create<JobPost>();
            int jobID = _fixture.Create<int>();
            jobPost.Id = jobID;

            _unitOfWork.JobPosts.Get(jobID).Returns(jobPost);

            //Act
            JobPostDTO tPosts = _service.Get(jobID);

            //Assert
            Assert.That(tPosts != null);

        }

        [Test]
        public void Get_should_return_job_Exeption()
        {
            try
            {
                //Arrange
                JobPost jobPost = _fixture.Create<JobPost>();
                int jobID = _fixture.Create<int>();
                jobPost.Id = jobID;

                //_unitOfWork.JobPosts.Get(jobID).Returns(jobPost);

                //Act
                JobPostDTO tPosts = _service.Get(jobID);

                //Assert
                Assert.That(tPosts != null);
            }
            catch (ValidationException ex)
            {
                Assert.AreEqual("Job post not found", ex.Message);
            }

        }


        [Test]
        public void ReviewResume_should_return_ReviewResume_no_null()
        {

            //Arrange
            JobPost jobPost = _fixture.Create<JobPost>();
            IEnumerable<SeekerResume> submitResume = _fixture.Create<IEnumerable<SeekerResume>>();
            jobPost.SubmitedResumes = submitResume;
            int jobID = _fixture.Create<int>();
            jobPost.Id = jobID;

            _unitOfWork.JobPosts.Get(jobID).Returns(jobPost);

            //Act
            IEnumerable<SeekerResumeDTO> tResume = _service.ReviewResumes(jobID);

            //Assert
            Assert.That(tResume != null);

        }

        [Test]
        public void ReviewResume_should_return_ReviewResume_Expectionpost()
        {
            try
            {
                //Arrange
                JobPost jobPost = _fixture.Create<JobPost>();
                IEnumerable<SeekerResume> submitResume = _fixture.Create<IEnumerable<SeekerResume>>();
                jobPost.SubmitedResumes = submitResume;
                int jobID = _fixture.Create<int>();
                jobPost.Id = jobID;

                //_unitOfWork.JobPosts.Get(jobID).Returns(jobPost);

                //Act
                IEnumerable<SeekerResumeDTO> tResume = _service.ReviewResumes(jobID);

                //Assert
                Assert.That(tResume != null);
            }

            catch (ValidationException ex)
            {
                Assert.AreEqual("Job post does not exist", ex.Message);
            }



        }

        [Test]
        public void ReviewResume_should_return_ReviewResume_ExpectionResume()
        {
            try
            {
                //Arrange
                JobPost jobPost = _fixture.Create<JobPost>();
                IEnumerable<SeekerResume> submitResume = _fixture.Create<IEnumerable<SeekerResume>>();
                // jobPost.SubmitedResumes = submitResume;
                int jobID = _fixture.Create<int>();
                jobPost.Id = jobID;

                _unitOfWork.JobPosts.Get(jobID).Returns(jobPost);

                //Act
                IEnumerable<SeekerResumeDTO> tResume = _service.ReviewResumes(jobID);

                //Assert
                Assert.That(tResume != null);
            }

            catch (ValidationException ex)
            {
                Assert.AreEqual("No resumes yet", ex.Message);
            }



        }

        [Test]
        public void Change_Change_Job_Post_no_null()
        {
            //Arrange
            JobPostDTO jobPostDTO = _fixture.Create<JobPostDTO>();

            JobPost jobPost = _fixture.Create<JobPost>();
            int jobPostID = _fixture.Create<int>();
            jobPostDTO.Id = jobPostID;
            jobPost.Id = jobPostID;


            _unitOfWork.JobPosts.Get(jobPostID).Returns(jobPost);


            //Act
            _service.Change(jobPostDTO);

            //Assert
            _unitOfWork.JobPosts.Received().Update(Arg.Is<JobPost>(p => p.SubmitedResumes != null
                                                 && p.SkillSets != null
                                                 && p.JobLocation != null));
        }



    }
}
