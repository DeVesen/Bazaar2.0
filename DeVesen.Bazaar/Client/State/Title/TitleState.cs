﻿using Fluxor;

namespace DeVesen.Bazaar.Client.State.Title;

[FeatureState]
public record TitleState(string Caption)
{
    private TitleState() : this(string.Empty) { }
}