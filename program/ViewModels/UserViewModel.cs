﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program.ViewModels
{
    public partial class UserViewModel : ObservableObject
    {
        IConnectivity connectivity;

        public UserViewModel(IConnectivity connectivity)
        {
            this.connectivity = connectivity;
        }
    }
}