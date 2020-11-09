using System;

public class Utility 
{
    public static short ToByte(float value)
    {
        if (value == 0f)
            return 0;

        //Check if negetive value
        bool negetive = false;
        if (value < 0)
        {
            negetive = true;
            value *= -1f;
        }            

        // Convert to fixed point
        int count = 0;
        while (value != Math.Floor(value))
        {
            value *= 10.0f;
            count++;
        }

        short result = (short)((count << 12) + (int)value);

        // If negetive insert 1 in last bit else insert 0
        if (!negetive)
            result = (short)(result << 2);
        else
        {
            result = (short)(result << 2);
            result += 1;
        }

        return result;
    }

    public static float FromByte(short value)
    {
        if (value == 0)
            return 0f;

        // Check last bit for sign and recover original value
        bool negetive = false;
        if (value % 2 == 0)
            value = (short)(value >> 2);
        else
        {
            negetive = true;
            value -= 1;
            value = (short)(value >> 2);
        }

        //Convert to floating point
        int count = value >> 12;
        float result = value & 0xfff;
        while (count > 0)
        {
            result /= 10.0f;
            count--;
        }

        // Add sign if negetive
        if (negetive)
            result *= -1f;

        return result;
    }
}
