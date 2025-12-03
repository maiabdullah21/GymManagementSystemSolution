using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace GymManagementDAL.Data.DataSeed
{
    public static class GymdbContextDataSeeding
    {
        public static bool SeedData(GymDBContext dbContext)
        {
            try
            {
                var HasPlans = dbContext.Plans.Any();
                var HasCategeories = dbContext.Categories.Any();
                if (HasCategeories && HasPlans) return false;
                if (!HasPlans)
                {
                    var plans = LoadDataFRomJasonFile<Plan>("plans.json");
                    if (plans.Any())
                    {
                        dbContext.Plans.AddRange(plans);
                    }
                }
                if (!HasCategeories)
                {
                    var Categeories = LoadDataFRomJasonFile<Category>("categories.json");
                    if (Categeories.Any())
                    {
                        dbContext.Categories.AddRange(Categeories);
                    }

                }
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception ex) { 
              Console.WriteLine($"Seeding Failed {ex}");
                return false;
            }

        }

        private static List<T> LoadDataFRomJasonFile<T>(string filename)
        {
            //C:\Users\maiab\OneDrive\Desktop\page items\GymManagementSystemSolution\GymManagementPL\wwwroot\files\categories.json
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", filename);
            if (!File.Exists(filepath)) throw new FileNotFoundException();
            string Data = File.ReadAllText(filepath);
            var Options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            return JsonSerializer.Deserialize<List<T>>(Data, Options)?? new List<T>();
        }
    }
}
