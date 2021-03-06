﻿using System;
using System.Collections.Generic;

namespace ControleDeRelease.Domain.Entities
{
    public class LiberacaoRelease
    {
        public int Id { get; set; }

        public Versao Versao { get; set; }

        public DateTime Data { get; set; }

        public List<ItemLiberacaoRelease> Itens { get; set; }
    }
}
