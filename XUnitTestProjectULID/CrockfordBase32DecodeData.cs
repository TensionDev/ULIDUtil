using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProjectULID
{
    public class CrockfordBase32DecodeData : TheoryData<Char, UInt64>
    {
        public CrockfordBase32DecodeData()
        {
            Add('0', 0);
            Add('o', 0);
            Add('O', 0);
            Add('1', 1);
            Add('i', 1);
            Add('I', 1);
            Add('l', 1);
            Add('L', 1);
            Add('2', 2);
            Add('3', 3);
            Add('4', 4);
            Add('5', 5);
            Add('6', 6);
            Add('7', 7);
            Add('8', 8);
            Add('9', 9);
            Add('a', 10);
            Add('A', 10);
            Add('b', 11);
            Add('B', 11);
            Add('c', 12);
            Add('C', 12);
            Add('d', 13);
            Add('D', 13);
            Add('e', 14);
            Add('E', 14);
            Add('f', 15);
            Add('F', 15);
            Add('g', 16);
            Add('G', 16);
            Add('h', 17);
            Add('H', 17);
            Add('j', 18);
            Add('J', 18);
            Add('k', 19);
            Add('K', 19);
            Add('m', 20);
            Add('M', 20);
            Add('n', 21);
            Add('N', 21);
            Add('p', 22);
            Add('P', 22);
            Add('q', 23);
            Add('Q', 23);
            Add('r', 24);
            Add('R', 24);
            Add('s', 25);
            Add('S', 25);
            Add('t', 26);
            Add('T', 26);
            Add('v', 27);
            Add('V', 27);
            Add('w', 28);
            Add('W', 28);
            Add('x', 29);
            Add('X', 29);
            Add('y', 30);
            Add('Y', 30);
            Add('z', 31);
            Add('Z', 31);
        }
    }
}
