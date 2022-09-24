# 📖 About

Arbolito is an experimental mapping tool designed to manipulate Grand Theft Auto V map files, it was created to make tedious tasks easier, so you can spend more time creating and less time doing boring and repetitive tasks.



![image](https://im4.ezgif.com/tmp/ezgif-4-9f8e77c8a4.gif)



# 🚀 Features

* YMAP Splitter
* YMAP Merger
* Train Tracks mover
* YNV to ONV converter
* Prop Replacer

# 🔧Requirements
* CodeWalker R30 dev44 or greater


# Usage
## 🪓YMAP Splitter
Splits YMAP files' entities either using a text file or YTYP, it creates new YMAP files with splitted entities and delete them from the base file which also can be found in the output folder.

⚠️ *It potentially can break YMAPs with LOD parenting, be careful with which YMAPs you are going to work with.*

![image](https://user-images.githubusercontent.com/20731612/192080274-b4ff5f7b-1eb4-4cfc-9caf-ee373d13a48f.png)

You can set all fields by clicking ```...``` buttons or copying and pasting paths directly on the text boxes.
Upon YMAP folder selection, the program will notify the user how many YMAPs were detected in the red label on the right-hand side

Splitting can be done using YTYP files or using text file with the following format (**One entity per line**):
```
prop1_helloworld
prop2_helloworld
prop3_helloworld
```

## 🪡YMAP Merger
Merges YMAP files (supports ```EntityDefs``` and ```CarGens```), it creates a new YMAP with the given name which can be found in the output folder.

⚠️ *It potentially can break YMAPs with LOD parenting, be careful with which YMAPs you are going to work with.*

![image](https://user-images.githubusercontent.com/20731612/192080646-b5bc2117-ef5e-40c1-9928-2a6dc583834c.png)

You can set all fields by clicking ```...``` buttons or copying and pasting paths directly on the text boxes.
Upon YMAP folder selection, the program will notify the user how many YMAPs were detected in the red label on the top.

Output YMAP name sets the output ymap filename

## 🛤️ Train Tracks Mover
Moves train tracks files ```Examples: trains1.dat, trains2.dat, trainse.dat```  along XYZ axis given an offset.

![image](https://user-images.githubusercontent.com/20731612/192080819-9b73e715-e594-4abd-87cb-974d461387ec.png)

You can set all fields by clicking ```...``` buttons or copying and pasting paths directly on the text boxes.
Upon Train Tracks folder selection, the program will notify the user how many Train Tracks were detected in the red label on the top.

*Set offset by writting numbers in XYZ fields, it supports decimals and negatives numbers.*


# 🚁 Contributing
Anyone can contribute to the project, feel free to open an issue to request a certain feature, or PR anything you think could be done better

# 🛣️ Roadmap
* Complete UI rework using Avalonia framework (currently being worked, planned for 0.7 release).
* .NET 7 Migration
* Switch to CodeX API when it comes out.

# 📗 License
Arbolito is licenced under the [MIT License](https://github.com/Hancapo/Arbolito/blob/master/LICENSE.txt)
