using GymManagementBLL.ViewModels.MemberViewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.interfaces
{
    internal interface IMemberService
    {
        IEnumerable<MemberViewModel> GetALLMembers();
        bool CreateMember(CreateMemberViewModel CreateMember);

        MemberViewModel? GetMemberDetails (int MemberId);
        HealthRecordViewModel? GetMemberHealthRecordDetails (int MemberId); 

        MemberToUpdateViewModel? GetMemberToUpdate (int MemberId);

        bool UpdateMemberDetails (int Id , MemberToUpdateViewModel MemberToUpdate);

        bool RemoveMember (int Id);
    }
}
