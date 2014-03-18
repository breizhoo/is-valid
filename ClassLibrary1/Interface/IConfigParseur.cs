﻿using System;
using System.IO;

namespace Domain.Interface
{
    public interface IConfigParseur
    {
        void Parse(FileInfo configFile, FileInfo originPath, Action<IConnectionStringItem> connectionStringsFind);
    }
}