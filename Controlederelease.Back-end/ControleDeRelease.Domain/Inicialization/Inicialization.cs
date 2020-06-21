using System;

namespace ControleDeRelease.Share.Inicialization
{
    public class Inicialization
    {
        public string DataBasePath => $@"{AppDomain.CurrentDomain.BaseDirectory}\database.db";
    }
}
