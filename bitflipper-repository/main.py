import random
import re
import time


def binary_to_decimal(binary):
    decimal, i, n = 0, 0, 0
    while binary != 0:
        dec = binary % 10
        decimal = decimal + dec * pow(2, i)
        binary = binary // 10
        i += 1
    return decimal


def string_input_to_decimal(input_string):
    array_str = " ".join(f"{ord(i):08b}" for i in input_string)

    byte_str = re.findall(r'\S+', array_str)

    index = random.randint(0, len(byte_str))

    flip_bits = byte_str[index].replace('1', 'x')  # replace 1 with x
    flip_bits = flip_bits.replace('0', '1')  # replace 0 with 1
    flip_bits = flip_bits.replace('x', '0')  # replace x with 0

    byte_str[index] = flip_bits

    str_data = ' '

    for_index = 0
    for i in byte_str:
        temp_data = int(byte_str[for_index])
        decimal_data = binary_to_decimal(temp_data)
        str_data = str_data + chr(decimal_data)
        for_index += 1

    return str_data


# re-input the updated string every 6 seconds. and then reset after 5 rotations
def main():
    with open('data/data.txt', 'r') as f:
        lines = f.readlines()

    oldSTR = lines
    x = 0

    while True:
        if x == 5:
            with open('data/data.txt', 'w', encoding="utf-8") as f:
                f.writelines(lines)
                x = 0;
        else:
            index = random.randint(0, len(lines))
            lines[index] = string_input_to_decimal(lines[index])
            with open('data/data.txt', 'w', encoding="utf-8") as f:
                f.writelines(lines)
            time.sleep(5)
            x += 1;


if __name__ == "__main__":
    main()
