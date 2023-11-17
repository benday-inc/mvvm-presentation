﻿using System;
using System.Linq;

namespace Benday.Presentation;

public interface ISelectableItem : ISelectable
{
    string Text { get; set; }
    string Value { get; set; }
    int Id { get; set; }
    string TooltipText { get; set; }
}


