using GymManagementBLL.services.interfaces;
using GymManagementBLL.ViewModels.MemberViewmodel;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.classes
{
    internal class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        // GenerecRepo<Member> MemberRepository = new GenerecRepo<Member>();
        public MemberService(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreateMember(CreateMemberViewModel CreateMember)
        {
           try
            {
               
                if (IsEmailExists(CreateMember.Email) || IsPhoneExists(CreateMember.Phone)) return false;


                var Member = new Member()
                {
                    Email = CreateMember.Email,
                    Phone = CreateMember.Phone,
                    Name = CreateMember.Name,
                    Gender = CreateMember.Gender,
                    DateOfBirth = CreateMember.DateOfBirth,
                    Address = new Address()
                    {
                        BuildingNumber = CreateMember.BuildingNumber,
                        City = CreateMember.City,
                        Street = CreateMember.street
                    },
                    HealthRecord = new HealthRecord()
                    {
                        Height = CreateMember.HealthRecord.Height,
                        Weight = CreateMember.HealthRecord.Weight,
                        BloodType = CreateMember.HealthRecord.BloodType,
                        Note = CreateMember.HealthRecord.Note
                    },

                };
                 _unitOfWork.GetGenerecRepo<Member>().Add(Member) ;
                return _unitOfWork.SaveChanges() > 0;
            }
            catch { return false; }
        }

        public IEnumerable<MemberViewModel> GetALLMembers()
        {
            var Members = _unitOfWork.GetGenerecRepo<Member>().GetAll();
            if (Members == null || Members.Any())
            {
                return Enumerable.Empty<MemberViewModel>();
            }
            var MemberViewModels = new List<MemberViewModel>();
            foreach (var Member in Members) {
                var memberviewmodel = new MemberViewModel()
                {
                    Id = Member.Id,
                    Name = Member.Name,
                    Phone = Member.Phone,
                    Email = Member.Email,
                    Photo = Member.Photo,
                    Gender = Member.Gender.ToString()
                };
                MemberViewModels.Add(memberviewmodel);

            }
            return MemberViewModels;
        }

        public MemberViewModel? GetMemberDetails(int MemberId)
        {
            var member = _unitOfWork.GetGenerecRepo<Member>().GetbyId(MemberId);
            if (member == null) return null;

            var viewmodel = new MemberViewModel()
            {
               
                Name = member.Name,
                Phone = member.Phone,
                Email = member.Email,
                Photo = member.Photo,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
                Address = $"{member.Address.BuildingNumber}  - {member.Address.Street} - {member.Address.City}"
            };

            var Activemembership = _unitOfWork.GetGenerecRepo<Membership>().GetAll(X => X.MemberId == MemberId && X.status == "Active").FirstOrDefault();
            if (Activemembership is not null)
            {
                viewmodel.MemberShipSartDate = Activemembership.CreatedAt.ToShortDateString();
                viewmodel.MemberShipEndDate = Activemembership.EndDate.ToShortDateString();
                var Plan = _unitOfWork.GetGenerecRepo<Plan>().GetbyId(Activemembership.PlanId);
                viewmodel.PlanName = Plan?.Name;
            }
            return viewmodel;
        }

        public HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId)
        {
            var MemberHealthRecord = _unitOfWork.GetGenerecRepo<HealthRecord>().GetbyId(MemberId);
            if (MemberHealthRecord == null) return null;

            return new HealthRecordViewModel()
            {
                Height = MemberHealthRecord.Height,
                Weight = MemberHealthRecord.Weight,
                BloodType = MemberHealthRecord.BloodType,
                Note = MemberHealthRecord.Note
            };
        }

        public MemberToUpdateViewModel? GetMemberToUpdate(int MemberId)
        {
            var member = _unitOfWork.GetGenerecRepo<Member>().GetbyId(MemberId);
            if (member == null) return null;
            return new MemberToUpdateViewModel()
            {
                Photo = member.Photo,
                 Name = member.Name,
                 Email = member.Email,
                 Phone = member.Phone,
                 BuildingNumber = member.Address.BuildingNumber,
                 street= member.Address.Street,
                 City = member.Address.City,
            };

        }

        public bool RemoveMember(int MemberId)
        {
            try
            {
                var member = _unitOfWork.GetGenerecRepo<Member>().GetbyId(MemberId);

                if (member == null) return false;
                var HasActiveMemberSession = _unitOfWork.GetGenerecRepo<MemberSession >().GetAll(X => X.MemberId == MemberId && X.Session.StartDate > DateTime.Now).Any();
                if (HasActiveMemberSession) return false;

                var mempership = _unitOfWork.GetGenerecRepo<Membership>().GetAll(X => X.MemberId == MemberId);
                if (mempership.Any())
                {
                    foreach (var mem in mempership)
                    {
                        _unitOfWork.GetGenerecRepo<Membership>().Delete(mem);
                    }

                }
                return _unitOfWork.SaveChanges() > 0;

            }
            catch {
                return false;
            }
        }

        public bool UpdateMemberDetails(int Id, MemberToUpdateViewModel MemberToUpdate)
        {
           try
            {

                if (IsEmailExists(MemberToUpdate.Email) || IsPhoneExists(MemberToUpdate.Phone)) return false;

                var member = _unitOfWork.GetGenerecRepo<Member>().GetbyId(Id);
                if (member == null) return false;

                member.Name = MemberToUpdate.Name;
                member.Email = MemberToUpdate.Email;
                member.Phone = MemberToUpdate.Phone;
                member.Address.BuildingNumber = MemberToUpdate.BuildingNumber;
                member .Address.Street = MemberToUpdate.street;
                member.Address.City = MemberToUpdate.City;
                member.UpdatedAt = DateTime.Now;

                 _unitOfWork.GetGenerecRepo<Member>().Update(member) ;
                return _unitOfWork.SaveChanges() > 0;

            }
            catch {
                return false;
                   
            }
        }

        #region Helper Method
        private bool IsEmailExists(string Email) {
            return _unitOfWork.GetGenerecRepo<Member>().GetAll(X => X.Email == Email).Any();

        }

        private bool IsPhoneExists(string Phone)
        {
            return _unitOfWork.GetGenerecRepo<Member>().GetAll(X => X.Phone == Phone).Any();

        }
        #endregion
    }
}
