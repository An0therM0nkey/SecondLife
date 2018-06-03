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
    }
}
