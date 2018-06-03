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
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                                    .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            JobPost jobPost = _fixture.Create<JobPost>();
            int Id = _fixture.Create<int>();
            _unitOfWork.JobPosts.Get(Id).Returns(jobPost);

            // Act
            _service.Delete(Id);

            // Assert
            _unitOfWork.JobPosts.Received(1).Delete(Id);
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
    }
}
