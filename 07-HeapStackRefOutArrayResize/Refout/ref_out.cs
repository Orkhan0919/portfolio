
using System;

#region Main




    int[] x = {1,2,3};
    int[] y= {4,5,6};
    int[] a;
    int[] b;
    
    Append(ref x,ref y);
    
    for (int i = 0; i < x.Length; i++)
    {
        Console.WriteLine(x[i]);
    }
    
    Append2(out a,out b);
    for (int i = 0; i < a.Length; i++)
    {
        Console.WriteLine(a[i]);
    }
    #endregion

    #region Append2

    void Append2(out int[] x, out int[] y)
    {
        int[] arr1 = {1, 2, 3};
        int[] arr2 = { 4, 5, 6 };
        int[] n = new int[arr1.Length + arr2.Length];

        for (int i = 0; i < arr1.Length; i++)
        {
            n[i] = arr1[i];
        }

        for (int i = 0; i < arr2.Length; i++)
        {
            n[arr1.Length + i] = arr2[i];
        }

        x = n;
        y = arr2;
    }
    #endregion

    #region Append
    
    int[] Append(ref int[] arr1, ref int[] arr2)
    {
        int[] newArr = new int[arr1.Length + arr2.Length];

        for (int i = 0; i < arr1.Length; i++)
        {
            newArr[i] = arr1[i];
        }

        for (int i = 0; i < arr2.Length; i++)
        {
            newArr[arr1.Length + i] = arr2[i];
        }

        arr1 = newArr;
        return newArr;
    }
    #endregion
