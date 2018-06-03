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
        public void Create_writes_no_null()
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

    }
}
