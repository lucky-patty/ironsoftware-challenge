# Ironsoftware Challenge
This is the task for the Iron Software challenge

## Main goal
Remember an old phone keypad? Well, this challenge replicated the way that keypad work.

### Requirement
- The input must end with `#`

### Example
This is the sample output

```
OldPhonePad(“33#”) => output: E
OldPhonePad(“227*#”) => output: B
OldPhonePad(“4433555 555666#”) => output: HELLO
```

### Extra features
- Support thai keyboard
- Add test cases

### Run 
```
cd ironsoftware-challenge/
dotnet run
```

## Test Case
I add some test cases for each keyboard layout `TH/ENG`

### Instructions
```
cd ironsoftware-tests
dotnet test
```

## Branches
I migrate the current migration into a new `master` while safely migrating an old master branch into `old_feature`.
```
Migration - Use it for migrate the branch
Master - Main branch
Old_feature - Old branch before adding XUnit and Ninject 
```
