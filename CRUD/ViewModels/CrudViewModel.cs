using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRUD.Models;
using CRUD.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace CRUD.ViewModels
{
    public sealed partial class CrudViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Wish> wishes = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddWishCommand))]
        private string wishEntry = "";

        public CrudViewModel() 
        {
            RefreshWishesFromDB();
        }

        [RelayCommand(CanExecute = nameof(AddWishCanExecute))]
        private async Task AddWish(string definition)
        {
            var wish = new Wish { Definition = definition };
            using (var dbContext = new AladdinContext())
            {
                dbContext.Add(wish);
                await dbContext.SaveChangesAsync();
            }
            Wishes.Add(wish);
            WishEntry = "";
        }

        private bool AddWishCanExecute()
        {
            return !string.IsNullOrEmpty(WishEntry);
        }

        private void RefreshWishesFromDB(AladdinContext? context = null)
        {
            Wishes.Clear();
            using(var dbContext = context??new AladdinContext())
            {
                foreach (var dbWish in dbContext.Wishes)
                {
                    Wishes.Add(dbWish);
                }
            }
        }

        [RelayCommand]
        private async Task Edit(Wish wish)
        {
            Trace.WriteLine($"Editing {wish}");

            string updateDefinition = await Shell.Current.DisplayPromptAsync(title: "Modifier", message: "", placeholder: wish.Definition);

            if(updateDefinition != null) 
            { 
                using(var dbContext = new AladdinContext())
                {
                    await dbContext.Wishes
                        .Where(dbWish => dbWish.Id == wish.Id)
                        .ExecuteUpdateAsync(setters => setters.SetProperty(dbWish => dbWish.Definition, updateDefinition));
                    
                    RefreshWishesFromDB(dbContext);
                }
            }
        }

        [RelayCommand]
        private async Task Delete(Wish wish)
        {
            Trace.WriteLine($"Deleting {wish}");
            using(var dbContext = new AladdinContext())
            {
                await dbContext.Wishes
                    .Where(dbWish => dbWish.Id == wish.Id)
                    .ExecuteDeleteAsync();

                RefreshWishesFromDB(dbContext);
            }
        }
    }
}