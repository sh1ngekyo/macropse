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
    * [Run](#run)
    * [SendKey](#sendkey)
    * [Delay](#delay-1)
    * [MoveMouseTo](#movemouseto)
    * [ShowMsgBox](#showmsgbox)
    * [Exit](#exit)
    * [LeftClick](#leftclick)
    * [RightClick](#rightclick)
    * [SendSignal](#sendsignal)
    * [MouseScroll](#mousescroll)
    * [VolumeAdd](#volumeadd)
    * [VolumeRemove](#volumeremove)
    * [VolumeMute](#volumemute)
    * [VolumeSet](#volumeset)

# Data types

## Number
Unsigned integer number.

Default value: 0;  
Min value: 0;  
Max value: 4294967295;

Example: `'0', '123', '7777'`

## String
The string type represents a sequence of zero or more Unicode characters.

Default value: None;  
Min value: None;  
Max value: None;

Example: `'This is text', 'Also text, but with special characters: []/,;\|{}.. and so on.'`

## Key
Represents a keyboard and mouse key codes or key aliases.

Default value: None;  
Min value: None;  
Max value: None;

Example: `'A', 'F9', 'Space'`

## Boolean
Represents a Boolean value.

Default value: false;  
Min value: false;  
Max value: true;

Example: `'true', 'false'`

# Root

The main tag of any script.  
Here are the global settings (options).

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

Accepts the name of the process. 

The script will only work if the window of this process is active.

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

The main tag of any script. 

Here are the global settings (options).
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

Signature: `"keys="{Key[] keys}"`

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
	<macro name="'Do work after press key F'" keys="F">
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

* Run
* Sendkey
* Delay
* MoveMouseTo,
* ShowMsgBox
* Exit
* LeftClick
* RightClick
* SendSignal
* MouseScroll
* VolumeAdd
* VolumeRemove
* VolumeMute
* VolumeSet

## Run

Run certain process by it's name.

Signature: `<command type="Run" params="{String procName, optional: Boolean asAdmin}" loop="..."/>`

Required params: String procName;

Optional params: Boolean withAdmin - run as administrator;

Example of simple macro for run notepad and cmd as admin if key A was pressed:
```xml
<root>
	<macro  keys="A">
		<command type="Run" params="'notepad.exe'"/>
		<command type="Run" params="'cmd.exe', true"/>
	</macro>
</root>
```

## SendKey

Emulates a keystroke.

Signature: `<command type="SendKey" params="{Key[] keys}" loop="..."/>`

Required params: Key[] keys;

Optional params: No;

Example of simple macro for press key B 10 times if key A was pressed:
```xml
<root>
	<macro  keys="A">
		<command type="SendKey" params="B" loop="10"/>
	</macro>
</root>
```
Output in notepad: `abbbbbbbbbb`;

Example of send word 'apple' if key A was pressed:
```xml
<root>
	<macro  keys="A">
		<command type="SendKey" params="P,P,L,E" loop="10"/>
	</macro>
</root>
```
Output in notepad: `apple`;

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
		<command type="SendKey" params="B"/>
	</macro>
</root>
```

## MoveMouseTo

Move mouse to certain screen location.

Signature: `<command type="MoveMouseTo" params="{Number x, Number y, Boolean usePixels}" loop="..."/>`

Required params: Number x, Number y;

Optional params: Boolean usePixels - true by default. If false - command will be use acceleration;

Example of simple macro for move mouse to position (960, 540) if key A was pressed:
```xml
<root>
	<macro  keys="A">
		<command type="MoveMouseTo" params="960, 540"/>
	</macro>
</root>
```

## ShowMsgBox

Show text in message box.

Signature: `<command type="ShowMsgBox" params="{String message}" loop="..."/>`

Required params: String message;

Optional params: No;

Example of simple macro for show message "hello world!" if key A was pressed:
```xml
<root>
	<macro  keys="A">
		<command type="ShowMsgBox" params="'hello world!'"/>
	</macro>
</root>
```

## Exit

Stop script execution.

Signature: `<command type="Exit"/>`

Required params: No;

Optional params: No;

Example. Stop script after show message:
```xml
<root>
	<macro  keys="A">
		<command type="ShowMsgBox" params="'hello world!'"/>
		<command type="Exit"/>
	</macro>
</root>
```

## LeftClick

Send left mouse click.

Signature: `<command type="LeftClick" loop="..."/>`

Required params: No;

Optional params: No;

Example of double click:
```xml
<root>
	<macro  keys="LButton">
		<command type="LeftClick"/>
	</macro>
</root>
```

## RightClick

Send right mouse click.

Signature: `<command type="RightClick" loop="..."/>`

Required params: No;

Optional params: No;

Example of double click:
```xml
<root>
	<macro  keys="RButton">
		<command type="RightClick"/>
	</macro>
</root>
```

## SendSignal

Make a sound.

Signature: `<command type="SendSignal" params="{Number frequency, Number duration}" loop="..."/>`

Required params: Number frequency. From 37 Hz to 32767 Hz;

Optional params: Number duration. In milliseconds. Min value - 1. By default - 1000(1 sec);

Example:
```xml
<root>
	<macro  keys="A">
		<command type="SendSignal" params="800, 1000"/>
	</macro>
</root>
```

## MouseScroll

Scroll mouse up/down.

Signature: `<command type="MouseScroll" params="{String direction}" loop="..."/>`

Required params: String direction. Allowed: 'up','down';

Optional params: No;

Example:
```xml
<root>
	<macro  keys="U">
		<command type="MouseScroll" params="'up'"/>
	</macro>
	<macro  keys="D">
		<command type="MouseScroll" params="'down'"/>
	</macro>
</root>
```

## VolumeAdd

Increase volume level in %.

Signature: `<command type="VolumeAdd" params="{Number value}" loop="..."/>`

Required params: Number value;

Optional params: No;

Example of increasing volume level by 1% with each run:
```xml
<root>
	<macro  keys="A">
		<command type="VolumeAdd" params="1"/>
	</macro>
</root>
```

## VolumeRemove

Decrease volume level in %.

Signature: `<command type="VolumeRemove" params="{Number value}" loop="..."/>`

Required params: Number value;

Optional params: No;

Example of decreasing volume level by 1% with each run:
```xml
<root>
	<macro  keys="A">
		<command type="VolumeRemove" params="1"/>
	</macro>
</root>
```

## VolumeMute

Mute volume.

Signature: `<command type="VolumeMute" loop="..."/>`

Required params: No;

Optional params: No;

Example of mute and unmute after 2 sec pause:
```xml
<root>
	<macro  keys="A">
		<command type="VolumeMute"/>
		<command type="Delay" params="2000"/>
		<command type="VolumeMute"/>
	</macro>
</root>
```

## VolumeSet

Set volume level in %.

Signature: `<command type="VolumeSet" params="{Number value}" loop="..."/>`

Required params: Number value;

Optional params: No;

Example of setting volume level by 100%:
```xml
<root>
	<macro  keys="A">
		<command type="VolumeSet" params="100"/>
	</macro>
</root>
```