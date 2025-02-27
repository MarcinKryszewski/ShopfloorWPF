using Shopfloor.Models.Trainings;
using Shopfloor.Features.Actions.ActionTransfer.Utilities;

namespace Shopfloor.Tests.Features.Actions.ActionTransfer.Utilities
{
    public class TrainingStatusRetrieverTests
    {
        [Fact]
        public void GetTrainingStatus_ShouldReturnUntrained_WhenThereIsNoTrainings()
        {
            // Arrange
            List<Training> trainingList = [];
            // Act
            TrainingStatus result = TrainingStatusRetriever.GetTrainingStatus(trainingList);
            // Assert
            result.ShouldBe(TrainingStatus.Untrained);
        }
        [Fact]
        public void GetTrainingStatus_ShouldReturnInTraining_WhenThereIsNoConfirmedByTraineeTrainings()
        {
            // Arrange
            List<Training> trainingList = [
                new Training {
                    Id = 1,
                    ActivityId = 1,
                    TraineeId = 1,
                    TrainingDate = DateTime.Now },];
            // Act
            TrainingStatus result = TrainingStatusRetriever.GetTrainingStatus(trainingList);
            // Assert
            result.ShouldBe(TrainingStatus.InTraining);
        }
        [Fact]
        public void GetTrainingStatus_ShouldReturnTrained_WhenThereIsConfirmedByTraineeTrainings()
        {
            // Arrange
            List<Training> trainingList = [
                new Training {
                    Id = 2,
                    ActivityId = 1,
                    TraineeId = 1,
                    TrainingDate = DateTime.Now },
                new Training {
                    Id = 1,
                    ActivityId = 1,
                    TraineeId = 1,
                    TrainingDate = DateTime.Now },
                new Training {
                    Id = 2,
                    ActivityId = 1,
                    TraineeId = 1,
                    TrainingDate = DateTime.Now },];
            // Act
            TrainingStatus result = TrainingStatusRetriever.GetTrainingStatus(trainingList);
            // Assert
            result.ShouldBe(TrainingStatus.Trained);
        }
    }
}