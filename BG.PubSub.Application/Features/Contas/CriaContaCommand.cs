﻿using BG.PubSub.Application.Abstractions;

namespace BG.PubSub.Application.Features.Contas;

public record CriaContaCommand(string Nome, string Documento) : ICommand;
