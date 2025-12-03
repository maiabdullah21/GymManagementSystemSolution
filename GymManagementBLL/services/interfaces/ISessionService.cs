using GymManagementBLL.ViewModels.SessionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionById (int id);
        bool CreateSession (CreateSessionViewModel createsession);
        UpdateSessionViewModel? SessionToUpdate (int SessionId);
        bool UpdateSession(UpdateSessionViewModel updatesession, int SessionId);
        bool DRemoveSession (int SessionId);
    }
}
