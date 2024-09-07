# 使用Unity制作的个人网站

## 简介

本项目是使用Unity游戏引擎制作的个人网站

本项目的制作原因是在B站看到[卑微美工Garry](https://www.bilibili.com/video/BV1HF411z7Pf?p=2&spm_id_from=333.880.my_history.page.click)
使用cocos游戏引擎制作了属于他个人的主页，并将自己的作品展示在其中，于是我就想使用同为游戏引擎的Unity制作一个属于自己的个人主页

> 注：本项目中所使用的图形素材均来自[卑微美工Garry](https://www.bilibili.com/video/BV1HF411z7Pf?p=2&spm_id_from=333.880.my_history.page.click)

## 网站配置

在本项目中，我将网站中的需要配置的文件进行了抽离，并将其存储在了[配置文件](docs/StreamingAssets/config.json)中，以便于网站的维护

### 部署

如需部署网站，只需将docs文件夹部署到服务器或对象存储中，直接访问[index.html](docs/index.html)即可直接访问网站

### 内容更改

如需更改网站加载时内容，可直接更改[index.html](docs/index.html)文件中的内容  
如需更改网站内容，可直接更改[配置文件](docs/StreamingAssets/config.json)中的内容

配置文件示例：

```json5 
{
    // 指针图标
    "Pointer": {
        //指针图标的路径
        "NormalUrl": "Image/Pointer/Normal.png",
        //悬浮时指针图标的路径
        "SuspensionUrl": "Image/Pointer/Suspension.png"
    },
    // 网页左上角的欢迎文字
    "Name": "HI I'M \nChengRui",
    // 欢迎文字下方的简介文字
    "Introduce": "AN College Students",
    // 外部链接
    "Links": [
        {
            // 链接文字
            "Text": "博客 <size=30>Blog",
            // 链接地址
            "Url": "https://www.ccrui.cn"
        },
        {
            "Text": "简历 <size=30>Resume",
            "Url": "https://s.ccrui.cn/zPOZd"
        },
        {
            "Text": "图床 <size=30>Graphic Bed",
            "Url": "https://tc.ccrui.cn/"
        },
        {
            "Text": "云盘 <size=30>Cloud Disk",
            "Url": "https://yun.ccrui.cn/"
        }
    ],
    // 按钮
    "Buttons": [
        {
            // 按钮图片
            "ImageUrl": "Image/Buttons/E-mail.png",
            // 鼠标悬浮时的按钮图片
            "ImagePressUrl": "Image/Buttons/E-mailPress.png",
            "Url": "mailto:296529530@qq.com"
        },
        {
            "ImageUrl": "Image/Buttons/Github.png",
            "ImagePressUrl": "Image/Buttons/GithubPress.png",
            "Url": "https://github.com/cr-zhichen"
        }
    ],
    // 底部信息
    "Informations": [
        {
            // 信息文字
            "Text": "张程瑞ZGCCRUI - COPYRIGHT©2022",
            // 信息链接
            "Url": "https://www.ccrui.cn/"
        },
        {
            "Text": "黑ICP备19008070号",
            "Url": "https://www.miit.gov.cn/"
        }
    ],
    //右侧跟随的装饰图片 数量不可增加
    "Decorate": {
        "0": {
            // 图片地址
            "ImageUrl": "Image/Decorate/0.png",
            // 对应鼠标移动距离 数值越大越靠慢
            "MobileDistanceThan": 50
        },
        "1": {
            "ImageUrl": "Image/Decorate/1.png",
            "MobileDistanceThan": 45
        },
        "2": {
            "ImageUrl": "Image/Decorate/2.png",
            "MobileDistanceThan": 20
        },
        "3": {
            "ImageUrl": "Image/Decorate/3.png",
            "MobileDistanceThan": 10
        },
        "4": {
            "ImageUrl": "Image/Decorate/4.png",
            "MobileDistanceThan": 5
        }
    },
    //背景
    "Background": {
        //背景图片地址
        "ImageUrl": "Image/Background/Bg.png",
        //背景颜色
        "Color": "#1B1B28",
        //杂色遮罩地址列表
        "MaskListUrl": [
            "Image/Background/Masks/NoiseB1.png",
            "Image/Background/Masks/NoiseB2.png",
            "Image/Background/Masks/NoiseLineB1.png",
            "Image/Background/Masks/NoiseLineB1.png",
            "Image/Background/Masks/NoiseLineEPT.png",
            "Image/Background/Masks/NoiseLineW.png",
            "Image/Background/Masks/NoiseW.png"
        ],
        //杂色遮罩的循环间隔
        "MaskIntervalTime": 0.1,
        //杂色遮罩颜色乘数
        "MaskColorMultiplier": "#1E1E2C"
    }
}
```

## 网站展示

[WebGl展示](https://cr-zhichen.github.io/PersonalHomePage/)

https://user-images.githubusercontent.com/57337795/165060559-22a2b4e2-f69a-4dc7-b5af-7e44614f9547.mp4
