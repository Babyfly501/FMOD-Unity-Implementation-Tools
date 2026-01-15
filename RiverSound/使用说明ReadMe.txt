#核心功能：
让FMOD Emitter在Spline上移动的同时，始终与目标Game Object的距离保持最短

#使用方式：
在Unity里新建一个Game Object，在它的Inspector里添加Spline Container组件
新建另一个Game Object，将脚本添加上去，
在Inspector里选择有Spline Container组件的Game Object作为Path
根据游戏需要，选择对应的GameObject作为Player（需要Emitter跟随的物件）