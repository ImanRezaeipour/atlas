﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.RequestFlow
{
    public interface IRequestUIValidation
    {
        void DoValidate(Request request);

    }
}
