# MIUI+窗口大小调整工具
可以调整MIUI+跨屏共享窗口的轻量化工具。

核心代码见[WindowSizeChangerCore](https://github.com/RF103T/WindowSizeChangerCore)

# 功能
调整MIUI+窗口大小，除了连接手机的页面，其他页面都可以调整。

# 使用方法
### GitHub版：
1. 下载最新的Release中的MIUIPlusWindowSizeChanger.zip压缩包。
2. 把其中的文件夹解压到你喜欢的地方。
3. 在界面四周按住鼠标左键拖动，即可调整窗口大小。

### Microsoft Store版：
在Microsoft Store中搜索`MIUI+窗口调整器`，安装即可。

### 设置说明：
标题栏阈值：仅在投屏界面生效（因为这个界面需要通过双击两次标题栏（窗口最上面的部分）来让窗口重新绘制），指的是双击位置在投屏窗口宽度上的比例。比如窗口宽度为200像素，设置值为0.1，那么就是在从标题栏开始20像素的位置进行双击操作。

边框响应宽度：指的是MIUI+窗口四周边框能够响应调整边框动作的宽度，单位是像素。比如设置为5，就代表距离窗口边缘5像素内能够响应调整边框的动作。


# 注意
+ 无论从哪个地方拖动，都只能从窗口右下角调整大小。
+ 需要自行安装[.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet/3.1/runtime)，推荐安装x64版本。

# MIUIPlusWindowSizeChanger
Change MIUI+ wndow size.

# License
[MIT](https://github.com/RF103T/MIUIPlusWindowSizeChanger/blob/master/LICENSE)
