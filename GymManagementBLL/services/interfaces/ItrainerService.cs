using GymManagementBLL.ViewModels.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.interfaces
{
    internal interface ItrainerService
    {
        IEnumerable<TrainerViewModel> GetAllTrainer();
        bool CreateTrainer(CreateTrainerViewModel createTrainer);
        bool UpdateTrainerDetailes (UpdateTrainerViewModel updateTrainer , int TrainerId);
        TrainerToUpdateViewModel? GetTrainerToUpdate (int  TrainerId);
        bool RemoveTrainer (int TrainerId);
        TrainerViewModel? GetTrainerDetailes (int TrainerId);
    }
}
