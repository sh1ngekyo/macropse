#  Macropse Docs

1. [Data types](#data-types)
    * [Numbers](#number)
    * [String](#string)
    * [Key](#key)
    * [Boolean](#boolean)
1. [Root](#root)
    * [Options](#options)
        * [Delay](#delay)
        * [Pause](#pause)
        * [IfWinActive](#ifwinactive)
        * [WhilePressed](#whilepressed)
1. [Macro](#macro)
    * [Keys](#keys)
    * [Name](#name)
    * [Loop](#loop)
3. [Commands](#commands)
    * [SendKey](#sendkey)
    * [Delay](#delay-1)
    * [ShowMsgBox](#showmsgbox)
    * [MoveMouseTo](#movemouseto)
    * [Run](#run)

# Data types

## Number
Unsigned integer number.

Default value: 0;
Min value: 0;
Max value: 4294967295;

## String
The string type represents a sequence of zero or more Unicode characters.

Default value: None;
Min value: None;
Max value: None;

## Key
Represents a keyboard and mouse key codes or key aliases.

Default value: None;
Min value: None;
Max value: None;


## Boolean
Represents a Boolean value.

Default value: false;
Min value: false;
Max value: true;

# Root
The main tag of any script. Here are the global settings (options).
```xml
<root>
...
</root>
```
## Options

### Delay
Signature: `delay="{Number num}"`
Global delay between executing next macro.

Default value: 100;

Example:
```xml
<root delay="1000">
...
</root>
```
### Pause
Signature: `pause="{Key key}"`
Key for pause script.

Default value: Pause;

Example:
```xml
<root pause="F8">
...
</root>
```
### IfWinActive

Signature: `ifWinActive="{String winName}"`
Accepts the name of the process. The script will only work if the window of this process is active.

Default value: None;

Example:
```xml
<root ifWinActive="notepad.exe">
...
</root>
```
### WhilePressed
Signature: `whilePressed="{Boolean isPressing}"`
If true - handles keypresses without waiting for release.
If false - wait for key release after macro execution.

Default value: false;

Example:
```xml
<root whilePressed="false">
...
</root>
```

# Macro

Description of the macro, required and additional options.
The main tag of any script. Here are the global settings (options).
```xml
<root>
	<macro ...>
		...
	</macro>
</root>
```


## Keys
Required option.
Contains keys (binds) for activate this macro.

Signature: `"keys="{Keys[] keys}"`

Default value: None;

Example:
```xml
<root>
	<macro keys="Q, W, E, R">
		...
	</macro>
</root>
```


## Name
Additional option.

Signature: `"name="{String name}"`
Contains a name for this macro.

Default value: Empty;

Example:
```xml
<root>
	<macro name="Do work after press key F" keys="F">
		...
	</macro>
</root>
```

## Loop
Additional option.

Signature: `"loop="{Number num}"`
Number of macro repeats.

Default value: 1;

Example:
```xml
<root>
	<macro keys="F" loop="10">
		...
	</macro>
</root>
```

# Commands

Performs one specific action.

Signature: `<command type="..." params="..." loop="..."/>`

There are several types of commands:
* SendKey
* Run
* ShowMsgBox
* MoveMouseTo
* Delay

## SendKey

Emulates a keystroke.

Signature: `<command type="SendKey" params="{Key key}" loop="..."/>`

Required params: Key key;
Optional params: No;

Example of simple macro for press key B 10 times if key A was pressed:
```xml
<root>
	<macro  keys="A">
		<command type="SendKey" params="B" loop="10"/>
	</macro>
</root>
```

Output in notepad: `ab`;

## Delay

Add delay in script in milliseconds.

Signature: `<command type="Delay" params="{Number ms}" loop="..."/>`

Required params: Number ms;
Optional params: No;

Example of simple macro for press key B with delay 1.5 seconds if key A was pressed:
```xml
<root>
	<macro  keys="A">
		<command type="Delay" params="1500"/>
		<command type="SendKey" params="B" loop="10"/>
	</macro>
</root>
```

## SendMsgBox

Show text in message box.

Signature: `<command type="ShowMsgBox" params="{String message}" loop="..."/>`

Required params: String message;
Optional params: No;

Example of simple macro for show message "hello" if key A was pressed:
```xml
<root>
	<macro  keys="A">
		<command type="ShowMsgBox" params="hello"/>
	</macro>
</root>
```

## MoveMouseTo

Move mouse to certain screen location.

Signature: `<command type="MoveMouseTo" params="{Number x, Number y}" loop="..."/>`

Required params: Number x, Number y;
Optional params: No;

Example of simple macro for move mouse to position (960, 540) if key A was pressed:
```xml
<root>
	<macro  keys="A">
		<command type="MoveMouseTo" params="960, 540"/>
	</macro>
</root>
```

## Run

Run certain process by it's name.

Signature: `<command type="Run" params="{String procName, optional: Boolean withAdmin}" loop="..."/>`

Required params: String procName;
Optional params: Boolean withAdmin - run as administrator;

Example of simple macro for run notepad and cmd as admin if key A was pressed:
```xml
<root>
	<macro  keys="A">
		<command type="Run" params="notepad.exe"/>
		<command type="Run" params="cmd.exe, true"/>
	</macro>
</root>
```