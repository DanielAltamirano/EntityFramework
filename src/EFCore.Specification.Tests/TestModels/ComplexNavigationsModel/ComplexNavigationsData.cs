﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.TestUtilities;

// ReSharper disable InconsistentNaming
namespace Microsoft.EntityFrameworkCore.TestModels.ComplexNavigationsModel
{
    public abstract class ComplexNavigationsData : IExpectedData
    {
        public IReadOnlyList<Level1> LevelOnes { get; }
        public IReadOnlyList<Level2> LevelTwos { get; }
        public IReadOnlyList<Level3> LevelThrees { get; }
        public IReadOnlyList<Level4> LevelFours { get; }

        public IReadOnlyList<Level1> SplitLevelOnes { get; }
        public IReadOnlyList<Level2> SplitLevelTwos { get; }
        public IReadOnlyList<Level3> SplitLevelThrees { get; }
        public IReadOnlyList<Level4> SplitLevelFours { get; }

        public abstract IQueryable<TEntity> Set<TEntity>()
            where TEntity : class;

        public ComplexNavigationsData()
        {
            LevelOnes = CreateLevelOnes(tableSplitting: false);
            LevelTwos = CreateLevelTwos(tableSplitting: false);
            LevelThrees = CreateLevelThrees(tableSplitting: false);
            LevelFours = CreateLevelFours(tableSplitting: false);

            WireUpPart1(LevelOnes, LevelTwos, LevelThrees, LevelFours, tableSplitting: false);
            WireUpInversePart1(LevelOnes, LevelTwos, LevelThrees, LevelFours, tableSplitting: false);

            WireUpPart2(LevelOnes, LevelTwos, LevelThrees, LevelFours, tableSplitting: false);
            WireUpInversePart2(LevelOnes, LevelTwos, LevelThrees, LevelFours, tableSplitting: false);

            SplitLevelOnes = CreateLevelOnes(tableSplitting: true);
            SplitLevelTwos = CreateLevelTwos(tableSplitting: true);
            SplitLevelThrees = CreateLevelThrees(tableSplitting: true);
            SplitLevelFours = CreateLevelFours(tableSplitting: true);

            WireUpPart1(SplitLevelOnes, SplitLevelTwos, SplitLevelThrees, SplitLevelFours, tableSplitting: true);
            WireUpInversePart1(SplitLevelOnes, SplitLevelTwos, SplitLevelThrees, SplitLevelFours, tableSplitting: true);

            WireUpPart2(SplitLevelOnes, SplitLevelTwos, SplitLevelThrees, SplitLevelFours, tableSplitting: true);
            WireUpInversePart2(SplitLevelOnes, SplitLevelTwos, SplitLevelThrees, SplitLevelFours, tableSplitting: true);
        }

        public static IReadOnlyList<Level1> CreateLevelOnes(bool tableSplitting)
        {
            var result = new List<Level1>
            {
                new Level1 { Id = 1, Name = "L1 01", Date = new DateTime(2001, 1, 1) },
                new Level1 { Id = 2, Name = "L1 02", Date = new DateTime(2002, 2, 2) },
                new Level1 { Id = 3, Name = "L1 03", Date = new DateTime(2003, 3, 3) },
                new Level1 { Id = 4, Name = "L1 04", Date = new DateTime(2004, 4, 4) },
                new Level1 { Id = 5, Name = "L1 05", Date = new DateTime(2005, 5, 5) },
                new Level1 { Id = 6, Name = "L1 06", Date = new DateTime(2006, 6, 6) },
                new Level1 { Id = 7, Name = "L1 07", Date = new DateTime(2007, 7, 7) },
                new Level1 { Id = 8, Name = "L1 08", Date = new DateTime(2008, 8, 8) },
                new Level1 { Id = 9, Name = "L1 09", Date = new DateTime(2009, 9, 9) },
                new Level1 { Id = 10, Name = "L1 10", Date = new DateTime(2010, 10, 10) }
            };

            if (!tableSplitting)
            {
                result.AddRange(
                    new List<Level1>
                    {
                        new Level1 { Id = 11, Name = "L1 11", Date = new DateTime(2009, 11, 11) },
                        new Level1 { Id = 12, Name = "L1 12", Date = new DateTime(2008, 12, 12) },
                        new Level1 { Id = 13, Name = "L1 13", Date = new DateTime(2007, 1, 1) }
                    });
            }

            foreach (var l1 in result)
            {
                l1.OneToMany_Optional = new List<Level2>();
                l1.OneToMany_Optional_Self = new List<Level1>();
                l1.OneToMany_Required = new List<Level2>();
                l1.OneToMany_Required_Self = new List<Level1>();
            }

            return result;
        }

        public static IReadOnlyList<Level2> CreateLevelTwos(bool tableSplitting)
        {
            var result = new List<Level2>
            {
                new Level2 { Id = 1, Name = "L2 01", Date = new DateTime(2010, 10, 10) },
                new Level2 { Id = 2, Name = "L2 02", Date = new DateTime(2002, 2, 2) },
                new Level2 { Id = 3, Name = "L2 03", Date = new DateTime(2008, 8, 8) },
                new Level2 { Id = 4, Name = "L2 04", Date = new DateTime(2004, 4, 4) },
                new Level2 { Id = 5, Name = "L2 05", Date = new DateTime(2006, 6, 6) },
                new Level2 { Id = 6, Name = "L2 06", Date = new DateTime(2005, 5, 5) },
                new Level2 { Id = 7, Name = "L2 07", Date = new DateTime(2007, 7, 7) },
                new Level2 { Id = 8, Name = "L2 08", Date = new DateTime(2003, 3, 3) },
                new Level2 { Id = 9, Name = "L2 09", Date = new DateTime(2009, 9, 9) },
                new Level2 { Id = 10, Name = "L2 10", Date = new DateTime(2001, 1, 1) }
            };

            if (!tableSplitting)
            {
                result.AddRange(
                    new List<Level2>
                    {
                        new Level2 { Id = 11, Name = "L2 11", Date = new DateTime(2000, 1, 1) }
                    });
            }

            foreach (var l2 in result)
            {
                l2.OneToMany_Optional = new List<Level3>();
                l2.OneToMany_Optional_Self = new List<Level2>();
                l2.OneToMany_Required = new List<Level3>();
                l2.OneToMany_Required_Self = new List<Level2>();
            }

            return result;
        }

        public static IReadOnlyList<Level3> CreateLevelThrees(bool tableSplitting)
        {
            var result = new List<Level3>
            {
                new Level3 { Id = 1, Name = "L3 01" },
                new Level3 { Id = 2, Name = "L3 02" },
                new Level3 { Id = 3, Name = "L3 03" },
                new Level3 { Id = 4, Name = "L3 04" },
                new Level3 { Id = 5, Name = "L3 05" },
                new Level3 { Id = 6, Name = "L3 06" },
                new Level3 { Id = 7, Name = "L3 07" },
                new Level3 { Id = 8, Name = "L3 08" },
                new Level3 { Id = 9, Name = "L3 09" },
                new Level3 { Id = 10, Name = "L3 10" }
            };

            foreach (var l3 in result)
            {
                l3.OneToMany_Optional = new List<Level4>();
                l3.OneToMany_Optional_Self = new List<Level3>();
                l3.OneToMany_Required = new List<Level4>();
                l3.OneToMany_Required_Self = new List<Level3>();
            }

            return result;
        }

        public static IReadOnlyList<Level4> CreateLevelFours(bool tableSplitting)
        {
            var result = new List<Level4>
            {
                new Level4 { Id = 1, Name = "L4 01" },
                new Level4 { Id = 2, Name = "L4 02" },
                new Level4 { Id = 3, Name = "L4 03" },
                new Level4 { Id = 4, Name = "L4 04" },
                new Level4 { Id = 5, Name = "L4 05" },
                new Level4 { Id = 6, Name = "L4 06" },
                new Level4 { Id = 7, Name = "L4 07" },
                new Level4 { Id = 8, Name = "L4 08" },
                new Level4 { Id = 9, Name = "L4 09" },
                new Level4 { Id = 10, Name = "L4 10" }
            };

            foreach (var l4 in result)
            {
                l4.OneToMany_Optional_Self = new List<Level4>();
                l4.OneToMany_Required_Self = new List<Level4>();
            }

            return result;
        }

        public static void WireUpPart1(
            IReadOnlyList<Level1> l1s, IReadOnlyList<Level2> l2s, IReadOnlyList<Level3> l3s, IReadOnlyList<Level4> l4s,
            bool tableSplitting)
        {
            l1s[0].OneToOne_Required_PK = l2s[0];
            l1s[1].OneToOne_Required_PK = l2s[1];
            l1s[2].OneToOne_Required_PK = l2s[2];
            l1s[3].OneToOne_Required_PK = l2s[3];
            l1s[4].OneToOne_Required_PK = l2s[4];
            l1s[5].OneToOne_Required_PK = l2s[5];
            l1s[6].OneToOne_Required_PK = l2s[6];
            l1s[7].OneToOne_Required_PK = l2s[7];
            l1s[8].OneToOne_Required_PK = l2s[8];
            l1s[9].OneToOne_Required_PK = l2s[9];
            if (!tableSplitting)
            {
                l1s[10].OneToOne_Required_PK = l2s[10];
            }

            if (tableSplitting)
            {
                l1s[0].OneToOne_Required_FK = l2s[0];
                l1s[1].OneToOne_Required_FK = l2s[1];
                l1s[2].OneToOne_Required_FK = l2s[2];
                l1s[3].OneToOne_Required_FK = l2s[3];
                l1s[4].OneToOne_Required_FK = l2s[4];
                l1s[5].OneToOne_Required_FK = l2s[5];
                l1s[6].OneToOne_Required_FK = l2s[6];
                l1s[7].OneToOne_Required_FK = l2s[7];
                l1s[8].OneToOne_Required_FK = l2s[8];
                l1s[9].OneToOne_Required_FK = l2s[9];
            }
            else
            {
                l1s[0].OneToOne_Required_FK = l2s[9];
                l1s[1].OneToOne_Required_FK = l2s[8];
                l1s[2].OneToOne_Required_FK = l2s[7];
                l1s[3].OneToOne_Required_FK = l2s[6];
                l1s[4].OneToOne_Required_FK = l2s[5];
                l1s[5].OneToOne_Required_FK = l2s[4];
                l1s[6].OneToOne_Required_FK = l2s[3];
                l1s[7].OneToOne_Required_FK = l2s[2];
                l1s[8].OneToOne_Required_FK = l2s[1];
                l1s[9].OneToOne_Required_FK = l2s[0];
                l1s[10].OneToOne_Required_FK = l2s[10];
            }

            l1s[0].OneToMany_Required = new List<Level2> { l2s[0], l2s[1], l2s[2], l2s[3], l2s[4], l2s[5], l2s[6], l2s[7], l2s[8], l2s[9] };

            if (!tableSplitting)
            {
                l1s[0].OneToMany_Required.Add(l2s[10]);
            }

            l1s[0].OneToMany_Required_Self = new List<Level1> { l1s[0], l1s[1] };
            if (!tableSplitting)
            {
                l1s[0].OneToMany_Required_Self.Add(l1s[11]);
            }
            l1s[1].OneToMany_Required_Self = new List<Level1> { l1s[2] };
            if (!tableSplitting)
            {
                l1s[1].OneToMany_Required_Self.Add(l1s[12]);
            }
            l1s[2].OneToMany_Required_Self = new List<Level1> { l1s[3] };
            l1s[3].OneToMany_Required_Self = new List<Level1> { l1s[4] };
            l1s[4].OneToMany_Required_Self = new List<Level1> { l1s[5] };
            l1s[5].OneToMany_Required_Self = new List<Level1> { l1s[6] };
            l1s[6].OneToMany_Required_Self = new List<Level1> { l1s[7] };
            l1s[7].OneToMany_Required_Self = new List<Level1> { l1s[8] };
            l1s[8].OneToMany_Required_Self = new List<Level1> { l1s[9] };
            l1s[9].OneToMany_Required_Self = new List<Level1>();
            if (!tableSplitting)
            {
                l1s[10].OneToMany_Required_Self = new List<Level1> { l1s[10] };
                l1s[11].OneToMany_Required_Self = new List<Level1>();
                l1s[12].OneToMany_Required_Self = new List<Level1>();
            }

            l2s[0].OneToOne_Required_PK = l3s[0];
            l2s[1].OneToOne_Required_PK = l3s[1];
            l2s[2].OneToOne_Required_PK = l3s[2];
            l2s[3].OneToOne_Required_PK = l3s[3];
            l2s[4].OneToOne_Required_PK = l3s[4];
            l2s[5].OneToOne_Required_PK = l3s[5];
            l2s[6].OneToOne_Required_PK = l3s[6];
            l2s[7].OneToOne_Required_PK = l3s[7];
            l2s[8].OneToOne_Required_PK = l3s[8];
            l2s[9].OneToOne_Required_PK = l3s[9];

            if (tableSplitting)
            {
                l2s[0].OneToOne_Required_FK = l3s[0];
                l2s[1].OneToOne_Required_FK = l3s[1];
                l2s[2].OneToOne_Required_FK = l3s[2];
                l2s[3].OneToOne_Required_FK = l3s[3];
                l2s[4].OneToOne_Required_FK = l3s[4];
                l2s[5].OneToOne_Required_FK = l3s[5];
                l2s[6].OneToOne_Required_FK = l3s[6];
                l2s[7].OneToOne_Required_FK = l3s[7];
                l2s[8].OneToOne_Required_FK = l3s[8];
                l2s[9].OneToOne_Required_FK = l3s[9];
            }
            else
            {
                l2s[0].OneToOne_Required_FK = l3s[9];
                l2s[1].OneToOne_Required_FK = l3s[8];
                l2s[2].OneToOne_Required_FK = l3s[7];
                l2s[3].OneToOne_Required_FK = l3s[6];
                l2s[4].OneToOne_Required_FK = l3s[5];
                l2s[5].OneToOne_Required_FK = l3s[4];
                l2s[6].OneToOne_Required_FK = l3s[3];
                l2s[7].OneToOne_Required_FK = l3s[2];
                l2s[8].OneToOne_Required_FK = l3s[1];
                l2s[9].OneToOne_Required_FK = l3s[0];
            }

            l2s[0].OneToMany_Required = new List<Level3> { l3s[0], l3s[1], l3s[2], l3s[3], l3s[4], l3s[5], l3s[6], l3s[7], l3s[8], l3s[9] };

            l2s[0].OneToMany_Required_Self = new List<Level2> { l2s[0], l2s[1] };
            if (!tableSplitting)
            {
                l2s[0].OneToMany_Required_Self.Add(l2s[10]);
            }
            l2s[1].OneToMany_Required_Self = new List<Level2> { l2s[2] };
            l2s[2].OneToMany_Required_Self = new List<Level2> { l2s[3] };
            l2s[3].OneToMany_Required_Self = new List<Level2> { l2s[4] };
            l2s[4].OneToMany_Required_Self = new List<Level2> { l2s[5] };
            l2s[5].OneToMany_Required_Self = new List<Level2> { l2s[6] };
            l2s[6].OneToMany_Required_Self = new List<Level2> { l2s[7] };
            l2s[7].OneToMany_Required_Self = new List<Level2> { l2s[8] };
            l2s[8].OneToMany_Required_Self = new List<Level2> { l2s[9] };
            l2s[9].OneToMany_Required_Self = new List<Level2>();
            if (!tableSplitting)
            {
                l2s[10].OneToMany_Required_Self = new List<Level2>();
            }

            l3s[0].OneToOne_Required_PK = l4s[0];
            l3s[1].OneToOne_Required_PK = l4s[1];
            l3s[2].OneToOne_Required_PK = l4s[2];
            l3s[3].OneToOne_Required_PK = l4s[3];
            l3s[4].OneToOne_Required_PK = l4s[4];
            l3s[5].OneToOne_Required_PK = l4s[5];
            l3s[6].OneToOne_Required_PK = l4s[6];
            l3s[7].OneToOne_Required_PK = l4s[7];
            l3s[8].OneToOne_Required_PK = l4s[8];
            l3s[9].OneToOne_Required_PK = l4s[9];

            if (tableSplitting)
            {
                l3s[0].OneToOne_Required_FK = l4s[0];
                l3s[1].OneToOne_Required_FK = l4s[1];
                l3s[2].OneToOne_Required_FK = l4s[2];
                l3s[3].OneToOne_Required_FK = l4s[3];
                l3s[4].OneToOne_Required_FK = l4s[4];
                l3s[5].OneToOne_Required_FK = l4s[5];
                l3s[6].OneToOne_Required_FK = l4s[6];
                l3s[7].OneToOne_Required_FK = l4s[7];
                l3s[8].OneToOne_Required_FK = l4s[8];
                l3s[9].OneToOne_Required_FK = l4s[9];
            }
            else
            {
                l3s[0].OneToOne_Required_FK = l4s[9];
                l3s[1].OneToOne_Required_FK = l4s[8];
                l3s[2].OneToOne_Required_FK = l4s[7];
                l3s[3].OneToOne_Required_FK = l4s[6];
                l3s[4].OneToOne_Required_FK = l4s[5];
                l3s[5].OneToOne_Required_FK = l4s[4];
                l3s[6].OneToOne_Required_FK = l4s[3];
                l3s[7].OneToOne_Required_FK = l4s[2];
                l3s[8].OneToOne_Required_FK = l4s[1];
                l3s[9].OneToOne_Required_FK = l4s[0];
            }

            l3s[0].OneToMany_Required = new List<Level4> { l4s[0], l4s[1], l4s[2], l4s[3], l4s[4], l4s[5], l4s[6], l4s[7], l4s[8], l4s[9] };

            l3s[0].OneToMany_Required_Self = new List<Level3> { l3s[0], l3s[1] };
            l3s[1].OneToMany_Required_Self = new List<Level3> { l3s[2] };
            l3s[2].OneToMany_Required_Self = new List<Level3> { l3s[3] };
            l3s[3].OneToMany_Required_Self = new List<Level3> { l3s[4] };
            l3s[4].OneToMany_Required_Self = new List<Level3> { l3s[5] };
            l3s[5].OneToMany_Required_Self = new List<Level3> { l3s[6] };
            l3s[6].OneToMany_Required_Self = new List<Level3> { l3s[7] };
            l3s[7].OneToMany_Required_Self = new List<Level3> { l3s[8] };
            l3s[8].OneToMany_Required_Self = new List<Level3> { l3s[9] };
            l3s[9].OneToMany_Required_Self = new List<Level3>();

            l4s[0].OneToMany_Required_Self = new List<Level4> { l4s[0], l4s[1] };
            l4s[1].OneToMany_Required_Self = new List<Level4> { l4s[2] };
            l4s[2].OneToMany_Required_Self = new List<Level4> { l4s[3] };
            l4s[3].OneToMany_Required_Self = new List<Level4> { l4s[4] };
            l4s[4].OneToMany_Required_Self = new List<Level4> { l4s[5] };
            l4s[5].OneToMany_Required_Self = new List<Level4> { l4s[6] };
            l4s[6].OneToMany_Required_Self = new List<Level4> { l4s[7] };
            l4s[7].OneToMany_Required_Self = new List<Level4> { l4s[8] };
            l4s[8].OneToMany_Required_Self = new List<Level4> { l4s[9] };
            l4s[9].OneToMany_Required_Self = new List<Level4>();
        }

        public static void WireUpInversePart1(
            IReadOnlyList<Level1> l1s, IReadOnlyList<Level2> l2s, IReadOnlyList<Level3> l3s, IReadOnlyList<Level4> l4s,
            bool tableSplitting)
        {
            l2s[0].OneToOne_Required_PK_Inverse = l1s[0];
            l2s[1].OneToOne_Required_PK_Inverse = l1s[1];
            l2s[2].OneToOne_Required_PK_Inverse = l1s[2];
            l2s[3].OneToOne_Required_PK_Inverse = l1s[3];
            l2s[4].OneToOne_Required_PK_Inverse = l1s[4];
            l2s[5].OneToOne_Required_PK_Inverse = l1s[5];
            l2s[6].OneToOne_Required_PK_Inverse = l1s[6];
            l2s[7].OneToOne_Required_PK_Inverse = l1s[7];
            l2s[8].OneToOne_Required_PK_Inverse = l1s[8];
            l2s[9].OneToOne_Required_PK_Inverse = l1s[9];
            if (!tableSplitting)
            {
                l2s[10].OneToOne_Required_PK_Inverse = l1s[10];
            }

            if (tableSplitting)
            {
                l2s[0].OneToOne_Required_FK_Inverse = l1s[0];
                l2s[1].OneToOne_Required_FK_Inverse = l1s[1];
                l2s[2].OneToOne_Required_FK_Inverse = l1s[2];
                l2s[3].OneToOne_Required_FK_Inverse = l1s[3];
                l2s[4].OneToOne_Required_FK_Inverse = l1s[4];
                l2s[5].OneToOne_Required_FK_Inverse = l1s[5];
                l2s[6].OneToOne_Required_FK_Inverse = l1s[6];
                l2s[7].OneToOne_Required_FK_Inverse = l1s[7];
                l2s[8].OneToOne_Required_FK_Inverse = l1s[8];
                l2s[9].OneToOne_Required_FK_Inverse = l1s[9];

                l2s[0].Level1_Required_Id = l1s[0].Id;
                l2s[1].Level1_Required_Id = l1s[1].Id;
                l2s[2].Level1_Required_Id = l1s[2].Id;
                l2s[3].Level1_Required_Id = l1s[3].Id;
                l2s[4].Level1_Required_Id = l1s[4].Id;
                l2s[5].Level1_Required_Id = l1s[5].Id;
                l2s[6].Level1_Required_Id = l1s[6].Id;
                l2s[7].Level1_Required_Id = l1s[7].Id;
                l2s[8].Level1_Required_Id = l1s[8].Id;
                l2s[9].Level1_Required_Id = l1s[9].Id;
            }
            else
            {
                l2s[9].OneToOne_Required_FK_Inverse = l1s[0];
                l2s[8].OneToOne_Required_FK_Inverse = l1s[1];
                l2s[7].OneToOne_Required_FK_Inverse = l1s[2];
                l2s[6].OneToOne_Required_FK_Inverse = l1s[3];
                l2s[5].OneToOne_Required_FK_Inverse = l1s[4];
                l2s[4].OneToOne_Required_FK_Inverse = l1s[5];
                l2s[3].OneToOne_Required_FK_Inverse = l1s[6];
                l2s[2].OneToOne_Required_FK_Inverse = l1s[7];
                l2s[1].OneToOne_Required_FK_Inverse = l1s[8];
                l2s[0].OneToOne_Required_FK_Inverse = l1s[9];
                l2s[10].OneToOne_Required_FK_Inverse = l1s[10];

                l2s[9].Level1_Required_Id = l1s[0].Id;
                l2s[8].Level1_Required_Id = l1s[1].Id;
                l2s[7].Level1_Required_Id = l1s[2].Id;
                l2s[6].Level1_Required_Id = l1s[3].Id;
                l2s[5].Level1_Required_Id = l1s[4].Id;
                l2s[4].Level1_Required_Id = l1s[5].Id;
                l2s[3].Level1_Required_Id = l1s[6].Id;
                l2s[2].Level1_Required_Id = l1s[7].Id;
                l2s[1].Level1_Required_Id = l1s[8].Id;
                l2s[0].Level1_Required_Id = l1s[9].Id;
                l2s[10].Level1_Required_Id = l1s[10].Id;
            }

            l2s[0].OneToMany_Required_Inverse = l1s[0];
            l2s[1].OneToMany_Required_Inverse = l1s[0];
            l2s[2].OneToMany_Required_Inverse = l1s[0];
            l2s[3].OneToMany_Required_Inverse = l1s[0];
            l2s[4].OneToMany_Required_Inverse = l1s[0];
            l2s[5].OneToMany_Required_Inverse = l1s[0];
            l2s[6].OneToMany_Required_Inverse = l1s[0];
            l2s[7].OneToMany_Required_Inverse = l1s[0];
            l2s[8].OneToMany_Required_Inverse = l1s[0];
            l2s[9].OneToMany_Required_Inverse = l1s[0];
            if (!tableSplitting)
            {
                l2s[10].OneToMany_Required_Inverse = l1s[0];
            }

            l1s[0].OneToMany_Required_Self_Inverse = l1s[0];
            l1s[1].OneToMany_Required_Self_Inverse = l1s[0];
            l1s[2].OneToMany_Required_Self_Inverse = l1s[1];
            l1s[3].OneToMany_Required_Self_Inverse = l1s[2];
            l1s[4].OneToMany_Required_Self_Inverse = l1s[3];
            l1s[5].OneToMany_Required_Self_Inverse = l1s[4];
            l1s[6].OneToMany_Required_Self_Inverse = l1s[5];
            l1s[7].OneToMany_Required_Self_Inverse = l1s[6];
            l1s[8].OneToMany_Required_Self_Inverse = l1s[7];
            l1s[9].OneToMany_Required_Self_Inverse = l1s[8];
            if (!tableSplitting)
            {
                l1s[11].OneToMany_Required_Self_Inverse = l1s[0];
                l1s[12].OneToMany_Required_Self_Inverse = l1s[1];
                l1s[10].OneToMany_Required_Self_Inverse = l1s[10];
            }

            l3s[0].OneToOne_Required_PK_Inverse = l2s[0];
            l3s[1].OneToOne_Required_PK_Inverse = l2s[1];
            l3s[2].OneToOne_Required_PK_Inverse = l2s[2];
            l3s[3].OneToOne_Required_PK_Inverse = l2s[3];
            l3s[4].OneToOne_Required_PK_Inverse = l2s[4];
            l3s[5].OneToOne_Required_PK_Inverse = l2s[5];
            l3s[6].OneToOne_Required_PK_Inverse = l2s[6];
            l3s[7].OneToOne_Required_PK_Inverse = l2s[7];
            l3s[8].OneToOne_Required_PK_Inverse = l2s[8];
            l3s[9].OneToOne_Required_PK_Inverse = l2s[9];

            if (tableSplitting)
            {
                l3s[0].OneToOne_Required_FK_Inverse = l2s[0];
                l3s[1].OneToOne_Required_FK_Inverse = l2s[1];
                l3s[2].OneToOne_Required_FK_Inverse = l2s[2];
                l3s[3].OneToOne_Required_FK_Inverse = l2s[3];
                l3s[4].OneToOne_Required_FK_Inverse = l2s[4];
                l3s[5].OneToOne_Required_FK_Inverse = l2s[5];
                l3s[6].OneToOne_Required_FK_Inverse = l2s[6];
                l3s[7].OneToOne_Required_FK_Inverse = l2s[7];
                l3s[8].OneToOne_Required_FK_Inverse = l2s[8];
                l3s[9].OneToOne_Required_FK_Inverse = l2s[9];

                l3s[0].Level2_Required_Id = l2s[0].Id;
                l3s[1].Level2_Required_Id = l2s[1].Id;
                l3s[2].Level2_Required_Id = l2s[2].Id;
                l3s[3].Level2_Required_Id = l2s[3].Id;
                l3s[4].Level2_Required_Id = l2s[4].Id;
                l3s[5].Level2_Required_Id = l2s[5].Id;
                l3s[6].Level2_Required_Id = l2s[6].Id;
                l3s[7].Level2_Required_Id = l2s[7].Id;
                l3s[8].Level2_Required_Id = l2s[8].Id;
                l3s[9].Level2_Required_Id = l2s[9].Id;
            }
            else
            {
                l3s[9].OneToOne_Required_FK_Inverse = l2s[0];
                l3s[8].OneToOne_Required_FK_Inverse = l2s[1];
                l3s[7].OneToOne_Required_FK_Inverse = l2s[2];
                l3s[6].OneToOne_Required_FK_Inverse = l2s[3];
                l3s[5].OneToOne_Required_FK_Inverse = l2s[4];
                l3s[4].OneToOne_Required_FK_Inverse = l2s[5];
                l3s[3].OneToOne_Required_FK_Inverse = l2s[6];
                l3s[2].OneToOne_Required_FK_Inverse = l2s[7];
                l3s[1].OneToOne_Required_FK_Inverse = l2s[8];
                l3s[0].OneToOne_Required_FK_Inverse = l2s[9];

                l3s[9].Level2_Required_Id = l2s[0].Id;
                l3s[8].Level2_Required_Id = l2s[1].Id;
                l3s[7].Level2_Required_Id = l2s[2].Id;
                l3s[6].Level2_Required_Id = l2s[3].Id;
                l3s[5].Level2_Required_Id = l2s[4].Id;
                l3s[4].Level2_Required_Id = l2s[5].Id;
                l3s[3].Level2_Required_Id = l2s[6].Id;
                l3s[2].Level2_Required_Id = l2s[7].Id;
                l3s[1].Level2_Required_Id = l2s[8].Id;
                l3s[0].Level2_Required_Id = l2s[9].Id;
            }

            l3s[0].OneToMany_Required_Inverse = l2s[0];
            l3s[1].OneToMany_Required_Inverse = l2s[0];
            l3s[2].OneToMany_Required_Inverse = l2s[0];
            l3s[3].OneToMany_Required_Inverse = l2s[0];
            l3s[4].OneToMany_Required_Inverse = l2s[0];
            l3s[5].OneToMany_Required_Inverse = l2s[0];
            l3s[6].OneToMany_Required_Inverse = l2s[0];
            l3s[7].OneToMany_Required_Inverse = l2s[0];
            l3s[8].OneToMany_Required_Inverse = l2s[0];
            l3s[9].OneToMany_Required_Inverse = l2s[0];

            l2s[0].OneToMany_Required_Self_Inverse = l2s[0];
            l2s[1].OneToMany_Required_Self_Inverse = l2s[0];
            if (!tableSplitting)
            {
                l2s[10].OneToMany_Required_Self_Inverse = l2s[0];
            }

            l2s[2].OneToMany_Required_Self_Inverse = l2s[1];
            l2s[3].OneToMany_Required_Self_Inverse = l2s[2];
            l2s[4].OneToMany_Required_Self_Inverse = l2s[3];
            l2s[5].OneToMany_Required_Self_Inverse = l2s[4];
            l2s[6].OneToMany_Required_Self_Inverse = l2s[5];
            l2s[7].OneToMany_Required_Self_Inverse = l2s[6];
            l2s[8].OneToMany_Required_Self_Inverse = l2s[7];
            l2s[9].OneToMany_Required_Self_Inverse = l2s[8];

            l4s[0].OneToOne_Required_PK_Inverse = l3s[0];
            l4s[1].OneToOne_Required_PK_Inverse = l3s[1];
            l4s[2].OneToOne_Required_PK_Inverse = l3s[2];
            l4s[3].OneToOne_Required_PK_Inverse = l3s[3];
            l4s[4].OneToOne_Required_PK_Inverse = l3s[4];
            l4s[5].OneToOne_Required_PK_Inverse = l3s[5];
            l4s[6].OneToOne_Required_PK_Inverse = l3s[6];
            l4s[7].OneToOne_Required_PK_Inverse = l3s[7];
            l4s[8].OneToOne_Required_PK_Inverse = l3s[8];
            l4s[9].OneToOne_Required_PK_Inverse = l3s[9];

            if (tableSplitting)
            {
                l4s[0].OneToOne_Required_FK_Inverse = l3s[0];
                l4s[1].OneToOne_Required_FK_Inverse = l3s[1];
                l4s[2].OneToOne_Required_FK_Inverse = l3s[2];
                l4s[3].OneToOne_Required_FK_Inverse = l3s[3];
                l4s[4].OneToOne_Required_FK_Inverse = l3s[4];
                l4s[5].OneToOne_Required_FK_Inverse = l3s[5];
                l4s[6].OneToOne_Required_FK_Inverse = l3s[6];
                l4s[7].OneToOne_Required_FK_Inverse = l3s[7];
                l4s[8].OneToOne_Required_FK_Inverse = l3s[8];
                l4s[9].OneToOne_Required_FK_Inverse = l3s[9];

                l4s[0].Level3_Required_Id = l3s[0].Id;
                l4s[1].Level3_Required_Id = l3s[1].Id;
                l4s[2].Level3_Required_Id = l3s[2].Id;
                l4s[3].Level3_Required_Id = l3s[3].Id;
                l4s[4].Level3_Required_Id = l3s[4].Id;
                l4s[5].Level3_Required_Id = l3s[5].Id;
                l4s[6].Level3_Required_Id = l3s[6].Id;
                l4s[7].Level3_Required_Id = l3s[7].Id;
                l4s[8].Level3_Required_Id = l3s[8].Id;
                l4s[9].Level3_Required_Id = l3s[9].Id;
            }
            else
            {
                l4s[9].OneToOne_Required_FK_Inverse = l3s[0];
                l4s[8].OneToOne_Required_FK_Inverse = l3s[1];
                l4s[7].OneToOne_Required_FK_Inverse = l3s[2];
                l4s[6].OneToOne_Required_FK_Inverse = l3s[3];
                l4s[5].OneToOne_Required_FK_Inverse = l3s[4];
                l4s[4].OneToOne_Required_FK_Inverse = l3s[5];
                l4s[3].OneToOne_Required_FK_Inverse = l3s[6];
                l4s[2].OneToOne_Required_FK_Inverse = l3s[7];
                l4s[1].OneToOne_Required_FK_Inverse = l3s[8];
                l4s[0].OneToOne_Required_FK_Inverse = l3s[9];

                l4s[9].Level3_Required_Id = l3s[0].Id;
                l4s[8].Level3_Required_Id = l3s[1].Id;
                l4s[7].Level3_Required_Id = l3s[2].Id;
                l4s[6].Level3_Required_Id = l3s[3].Id;
                l4s[5].Level3_Required_Id = l3s[4].Id;
                l4s[4].Level3_Required_Id = l3s[5].Id;
                l4s[3].Level3_Required_Id = l3s[6].Id;
                l4s[2].Level3_Required_Id = l3s[7].Id;
                l4s[1].Level3_Required_Id = l3s[8].Id;
                l4s[0].Level3_Required_Id = l3s[9].Id;
            }

            l4s[0].OneToMany_Required_Inverse = l3s[0];
            l4s[1].OneToMany_Required_Inverse = l3s[0];
            l4s[2].OneToMany_Required_Inverse = l3s[0];
            l4s[3].OneToMany_Required_Inverse = l3s[0];
            l4s[4].OneToMany_Required_Inverse = l3s[0];
            l4s[5].OneToMany_Required_Inverse = l3s[0];
            l4s[6].OneToMany_Required_Inverse = l3s[0];
            l4s[7].OneToMany_Required_Inverse = l3s[0];
            l4s[8].OneToMany_Required_Inverse = l3s[0];
            l4s[9].OneToMany_Required_Inverse = l3s[0];

            l3s[0].OneToMany_Required_Self_Inverse = l3s[0];
            l3s[1].OneToMany_Required_Self_Inverse = l3s[0];
            l3s[2].OneToMany_Required_Self_Inverse = l3s[1];
            l3s[3].OneToMany_Required_Self_Inverse = l3s[2];
            l3s[4].OneToMany_Required_Self_Inverse = l3s[3];
            l3s[5].OneToMany_Required_Self_Inverse = l3s[4];
            l3s[6].OneToMany_Required_Self_Inverse = l3s[5];
            l3s[7].OneToMany_Required_Self_Inverse = l3s[6];
            l3s[8].OneToMany_Required_Self_Inverse = l3s[7];
            l3s[9].OneToMany_Required_Self_Inverse = l3s[8];

            l4s[0].OneToMany_Required_Self_Inverse = l4s[0];
            l4s[1].OneToMany_Required_Self_Inverse = l4s[0];
            l4s[2].OneToMany_Required_Self_Inverse = l4s[1];
            l4s[3].OneToMany_Required_Self_Inverse = l4s[2];
            l4s[4].OneToMany_Required_Self_Inverse = l4s[3];
            l4s[5].OneToMany_Required_Self_Inverse = l4s[4];
            l4s[6].OneToMany_Required_Self_Inverse = l4s[5];
            l4s[7].OneToMany_Required_Self_Inverse = l4s[6];
            l4s[8].OneToMany_Required_Self_Inverse = l4s[7];
            l4s[9].OneToMany_Required_Self_Inverse = l4s[8];
        }

        public static void WireUpPart2(
            IReadOnlyList<Level1> l1s, IReadOnlyList<Level2> l2s, IReadOnlyList<Level3> l3s, IReadOnlyList<Level4> l4s,
            bool tableSplitting)
        {
            l1s[0].OneToOne_Optional_PK = l2s[0];
            l1s[2].OneToOne_Optional_PK = l2s[2];
            l1s[4].OneToOne_Optional_PK = l2s[4];
            l1s[6].OneToOne_Optional_PK = l2s[6];
            l1s[8].OneToOne_Optional_PK = l2s[8];

            l1s[1].OneToOne_Optional_FK = l2s[8];
            l1s[3].OneToOne_Optional_FK = l2s[6];
            l1s[5].OneToOne_Optional_FK = l2s[4];
            l1s[7].OneToOne_Optional_FK = l2s[2];
            l1s[9].OneToOne_Optional_FK = l2s[0];

            l1s[0].OneToMany_Optional = new List<Level2> { l2s[1], l2s[3], l2s[5], l2s[7], l2s[9] };

            l1s[1].OneToMany_Optional_Self = new List<Level1> { l1s[0] };
            l1s[3].OneToMany_Optional_Self = new List<Level1> { l1s[2] };
            l1s[5].OneToMany_Optional_Self = new List<Level1> { l1s[4] };
            l1s[7].OneToMany_Optional_Self = new List<Level1> { l1s[6] };
            l1s[9].OneToMany_Optional_Self = new List<Level1> { l1s[8] };

            l1s[0].OneToOne_Optional_Self = l1s[9];
            l1s[1].OneToOne_Optional_Self = l1s[8];
            l1s[2].OneToOne_Optional_Self = l1s[7];
            l1s[3].OneToOne_Optional_Self = l1s[6];
            l1s[4].OneToOne_Optional_Self = l1s[5];

            l2s[0].OneToOne_Optional_PK = l3s[0];
            l2s[2].OneToOne_Optional_PK = l3s[2];
            l2s[5].OneToOne_Optional_PK = l3s[4];
            l2s[7].OneToOne_Optional_PK = l3s[6];
            l2s[9].OneToOne_Optional_PK = l3s[8];

            l2s[1].OneToOne_Optional_FK = l3s[8];
            l2s[3].OneToOne_Optional_FK = l3s[6];
            l2s[4].OneToOne_Optional_FK = l3s[4];
            l2s[6].OneToOne_Optional_FK = l3s[2];
            l2s[8].OneToOne_Optional_FK = l3s[0];

            l2s[0].OneToMany_Optional = new List<Level3> { l3s[1], l3s[5], l3s[9] };
            l2s[1].OneToMany_Optional = new List<Level3> { l3s[3], l3s[7] };

            l2s[1].OneToMany_Optional_Self = new List<Level2> { l2s[0] };
            l2s[3].OneToMany_Optional_Self = new List<Level2> { l2s[2] };
            l2s[5].OneToMany_Optional_Self = new List<Level2> { l2s[4] };
            l2s[7].OneToMany_Optional_Self = new List<Level2> { l2s[6] };
            l2s[9].OneToMany_Optional_Self = new List<Level2> { l2s[8] };

            l2s[0].OneToOne_Optional_Self = l2s[9];
            l2s[1].OneToOne_Optional_Self = l2s[8];
            l2s[2].OneToOne_Optional_Self = l2s[7];
            l2s[3].OneToOne_Optional_Self = l2s[6];
            l2s[4].OneToOne_Optional_Self = l2s[5];

            l3s[0].OneToOne_Optional_PK = l4s[0];
            l3s[2].OneToOne_Optional_PK = l4s[2];
            l3s[4].OneToOne_Optional_PK = l4s[4];
            l3s[6].OneToOne_Optional_PK = l4s[6];
            l3s[8].OneToOne_Optional_PK = l4s[8];

            l3s[1].OneToOne_Optional_FK = l4s[8];
            l3s[3].OneToOne_Optional_FK = l4s[6];
            l3s[5].OneToOne_Optional_FK = l4s[4];
            l3s[7].OneToOne_Optional_FK = l4s[2];
            l3s[9].OneToOne_Optional_FK = l4s[0];

            l3s[0].OneToMany_Optional = new List<Level4> { l4s[1], l4s[3], l4s[5], l4s[7], l4s[9] };

            l3s[1].OneToMany_Optional_Self = new List<Level3> { l3s[0] };
            l3s[3].OneToMany_Optional_Self = new List<Level3> { l3s[2] };
            l3s[5].OneToMany_Optional_Self = new List<Level3> { l3s[4] };
            l3s[7].OneToMany_Optional_Self = new List<Level3> { l3s[6] };
            l3s[9].OneToMany_Optional_Self = new List<Level3> { l3s[8] };

            l3s[0].OneToOne_Optional_Self = l3s[9];
            l3s[1].OneToOne_Optional_Self = l3s[8];
            l3s[2].OneToOne_Optional_Self = l3s[7];
            l3s[3].OneToOne_Optional_Self = l3s[6];
            l3s[4].OneToOne_Optional_Self = l3s[5];

            l4s[1].OneToMany_Optional_Self = new List<Level4> { l4s[0] };
            l4s[3].OneToMany_Optional_Self = new List<Level4> { l4s[2] };
            l4s[5].OneToMany_Optional_Self = new List<Level4> { l4s[4] };
            l4s[7].OneToMany_Optional_Self = new List<Level4> { l4s[6] };
            l4s[9].OneToMany_Optional_Self = new List<Level4> { l4s[8] };
        }

        public static void WireUpInversePart2(
            IReadOnlyList<Level1> l1s, IReadOnlyList<Level2> l2s, IReadOnlyList<Level3> l3s, IReadOnlyList<Level4> l4s,
            bool tableSplitting)
        {
            l2s[0].OneToOne_Optional_PK_Inverse = l1s[0];
            l2s[2].OneToOne_Optional_PK_Inverse = l1s[2];
            l2s[4].OneToOne_Optional_PK_Inverse = l1s[4];
            l2s[6].OneToOne_Optional_PK_Inverse = l1s[6];
            l2s[8].OneToOne_Optional_PK_Inverse = l1s[8];

            l2s[8].OneToOne_Optional_FK_Inverse = l1s[1];
            l2s[6].OneToOne_Optional_FK_Inverse = l1s[3];
            l2s[4].OneToOne_Optional_FK_Inverse = l1s[5];
            l2s[2].OneToOne_Optional_FK_Inverse = l1s[7];
            l2s[0].OneToOne_Optional_FK_Inverse = l1s[9];

            l2s[8].Level1_Optional_Id = l1s[1].Id;
            l2s[6].Level1_Optional_Id = l1s[3].Id;
            l2s[4].Level1_Optional_Id = l1s[5].Id;
            l2s[2].Level1_Optional_Id = l1s[7].Id;
            l2s[0].Level1_Optional_Id = l1s[9].Id;

            l2s[1].OneToMany_Optional_Inverse = l1s[0];
            l2s[3].OneToMany_Optional_Inverse = l1s[0];
            l2s[5].OneToMany_Optional_Inverse = l1s[0];
            l2s[7].OneToMany_Optional_Inverse = l1s[0];
            l2s[9].OneToMany_Optional_Inverse = l1s[0];

            l1s[0].OneToMany_Optional_Self_Inverse = l1s[1];
            l1s[2].OneToMany_Optional_Self_Inverse = l1s[3];
            l1s[4].OneToMany_Optional_Self_Inverse = l1s[5];
            l1s[6].OneToMany_Optional_Self_Inverse = l1s[7];
            l1s[8].OneToMany_Optional_Self_Inverse = l1s[9];

            l3s[0].OneToOne_Optional_PK_Inverse = l2s[0];
            l3s[2].OneToOne_Optional_PK_Inverse = l2s[2];
            l3s[5].OneToOne_Optional_PK_Inverse = l2s[4];
            l3s[7].OneToOne_Optional_PK_Inverse = l2s[6];
            l3s[9].OneToOne_Optional_PK_Inverse = l2s[8];

            l3s[8].OneToOne_Optional_FK_Inverse = l2s[1];
            l3s[6].OneToOne_Optional_FK_Inverse = l2s[3];
            l3s[4].OneToOne_Optional_FK_Inverse = l2s[4];
            l3s[2].OneToOne_Optional_FK_Inverse = l2s[6];
            l3s[0].OneToOne_Optional_FK_Inverse = l2s[8];

            l3s[8].Level2_Optional_Id = l2s[1].Id;
            l3s[6].Level2_Optional_Id = l2s[3].Id;
            l3s[4].Level2_Optional_Id = l2s[4].Id;
            l3s[2].Level2_Optional_Id = l2s[6].Id;
            l3s[0].Level2_Optional_Id = l2s[8].Id;

            l3s[1].OneToMany_Optional_Inverse = l2s[0];
            l3s[5].OneToMany_Optional_Inverse = l2s[0];
            l3s[9].OneToMany_Optional_Inverse = l2s[0];
            l3s[3].OneToMany_Optional_Inverse = l2s[1];
            l3s[7].OneToMany_Optional_Inverse = l2s[1];

            l2s[0].OneToMany_Optional_Self_Inverse = l2s[1];
            l2s[2].OneToMany_Optional_Self_Inverse = l2s[3];
            l2s[4].OneToMany_Optional_Self_Inverse = l2s[5];
            l2s[6].OneToMany_Optional_Self_Inverse = l2s[7];
            l2s[8].OneToMany_Optional_Self_Inverse = l2s[9];

            l4s[0].OneToOne_Optional_PK_Inverse = l3s[0];
            l4s[2].OneToOne_Optional_PK_Inverse = l3s[2];
            l4s[4].OneToOne_Optional_PK_Inverse = l3s[4];
            l4s[6].OneToOne_Optional_PK_Inverse = l3s[6];
            l4s[8].OneToOne_Optional_PK_Inverse = l3s[8];

            l4s[8].OneToOne_Optional_FK_Inverse = l3s[1];
            l4s[6].OneToOne_Optional_FK_Inverse = l3s[3];
            l4s[4].OneToOne_Optional_FK_Inverse = l3s[5];
            l4s[2].OneToOne_Optional_FK_Inverse = l3s[7];
            l4s[0].OneToOne_Optional_FK_Inverse = l3s[9];

            l4s[8].Level3_Optional_Id = l3s[1].Id;
            l4s[6].Level3_Optional_Id = l3s[3].Id;
            l4s[4].Level3_Optional_Id = l3s[5].Id;
            l4s[2].Level3_Optional_Id = l3s[7].Id;
            l4s[0].Level3_Optional_Id = l3s[9].Id;

            l4s[1].OneToMany_Optional_Inverse = l3s[0];
            l4s[3].OneToMany_Optional_Inverse = l3s[0];
            l4s[5].OneToMany_Optional_Inverse = l3s[0];
            l4s[7].OneToMany_Optional_Inverse = l3s[0];
            l4s[9].OneToMany_Optional_Inverse = l3s[0];

            l3s[0].OneToMany_Optional_Self_Inverse = l3s[1];
            l3s[2].OneToMany_Optional_Self_Inverse = l3s[3];
            l3s[4].OneToMany_Optional_Self_Inverse = l3s[5];
            l3s[6].OneToMany_Optional_Self_Inverse = l3s[7];
            l3s[8].OneToMany_Optional_Self_Inverse = l3s[9];

            l4s[0].OneToMany_Optional_Self_Inverse = l4s[1];
            l4s[2].OneToMany_Optional_Self_Inverse = l4s[3];
            l4s[4].OneToMany_Optional_Self_Inverse = l4s[5];
            l4s[6].OneToMany_Optional_Self_Inverse = l4s[7];
            l4s[8].OneToMany_Optional_Self_Inverse = l4s[9];
        }

        public static void Seed(ComplexNavigationsContext context, bool tableSplitting = false)
        {
            var l1s = CreateLevelOnes(tableSplitting);
            var l2s = CreateLevelTwos(tableSplitting);
            var l3s = CreateLevelThrees(tableSplitting);
            var l4s = CreateLevelFours(tableSplitting);

            context.LevelOne.AddRange(l1s);

            WireUpPart1(l1s, l2s, l3s, l4s, tableSplitting);

            context.SaveChanges();

            WireUpPart2(l1s, l2s, l3s, l4s, tableSplitting);

            var globalizations = new List<ComplexNavigationGlobalization>();
            for (var i = 0; i < 10; i++)
            {
                var language = new ComplexNavigationLanguage { Name = "Language" + i, CultureString = "Foo" + i };
                var globalization = new ComplexNavigationGlobalization { Text = "Globalization" + i, Language = language };
                globalizations.Add(globalization);

                context.Languages.Add(language);
                context.Globalizations.Add(globalization);
            }

            var mls1 = new ComplexNavigationString { DefaultText = "MLS1", Globalizations = globalizations.Take(3).ToList() };
            var mls2 = new ComplexNavigationString { DefaultText = "MLS2", Globalizations = globalizations.Skip(3).Take(3).ToList() };
            var mls3 = new ComplexNavigationString { DefaultText = "MLS3", Globalizations = globalizations.Skip(6).Take(3).ToList() };
            var mls4 = new ComplexNavigationString { DefaultText = "MLS4", Globalizations = globalizations.Skip(9).ToList() };

            context.MultilingualStrings.AddRange(mls1, mls2, mls3, mls4);

            var field1 = new ComplexNavigationField { Name = "Field1", Label = mls1, Placeholder = null };
            var field2 = new ComplexNavigationField { Name = "Field2", Label = mls3, Placeholder = mls4 };

            context.Fields.AddRange(field1, field2);
            context.SaveChanges();
        }
    }
}
