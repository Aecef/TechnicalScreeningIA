# Technical Screening For IA

[How-To-Use](https://github.com/Aecef/TechnicalScreeningIA#how-to-use)

## How To Use

Here are two different options when running this program

### Through The Console

With a console open in `TechnicalScreeningIA\TechnicalScreeningIA\bin\Debug\net7.0\` :  

Run the command `.\TechnicalScreeningIA "[PATH]"\CombinedLetters`

The Console will print the contents of file that contains the list of Student Ids that had combined files.

![image](https://user-images.githubusercontent.com/56279192/216177820-b9d42ecb-5984-4623-a3b3-91337965b0c1.png)


### Moving the 'CombinedLetters' Directory  


Move the `CombinedLetters` folder into the same dir as the .exe :  

`TechnicalScreeningIA\TechnicalScreeningIA\bin\Debug\net7.0\` 

Double click `TechnicalScreeningIA.exe` to run the program.

![image](https://user-images.githubusercontent.com/56279192/216178632-07e5fb8e-ed39-4b3f-aaae-a7ab577ac53d.png)

The program will run and generate the files required.  
> **Warning**
> Currently there is no UI to indicate to the user what has happened.

## Folder Format

### Before Running `TechnicalScreeningIA.exe`

```mermaid
graph TD;
    TechnicalScreeningIA-->Input;
    TechnicalScreeningIA-->Output;
    TechnicalScreeningIA-->Archive;
    Input-->Admission;
    Input-->Scholarship;
    Admission-->Admission\20230201;
    Admission\20230201-->admission-#######1.txt;
    Admission\20230201-->admission-#######2.txt;
    Scholarship-->Scholarship\20230201;
    Scholarship\20230201-->scholarship-#######1.txt;
```
### After Running `TechnicalScreeningIA.exe`

```mermaid
graph TD;
    TechnicalScreeningIA-->Input;
    TechnicalScreeningIA-->Output;
    TechnicalScreeningIA-->Archive;
    Input-->Input\Admission;
    Input-->Input\Scholarship;
    Output-->Output\20230201;
    Output\20230201-->Combined;
    Combined-->admission-scholarship-#######1.txt;
    Combined-->combination-report-20230201.txt;
    Output\20230201-->Uncombined;
    Uncombined-->Uncombined\admission-#######2.txt;
    Archive-->Archive\Admission;
    Archive-->Archive\Scholarship;
    Archive\Admission-->admission-#######1.txt;
    Archive\Admission-->admission-#######2.txt;
    Archive\Scholarship-->scholarship-#######1.txt;
    

```

 
