# Lunar.JigsawPuzzleCaptcha

<div align="center">
    <img src="https://gclstorage.blob.core.windows.net/images/Lunar.JigsawPuzzleCaptcha-banner.png" />
</div>

[![Azure Static Web Apps CI/CD](https://github.com/goh-chunlin/Lunar.JigsawPuzzleCaptcha/actions/workflows/azure-static-web-apps-witty-bush-03a90ff00.yml/badge.svg)](https://github.com/goh-chunlin/Lunar.JigsawPuzzleCaptcha/actions/workflows/azure-static-web-apps-witty-bush-03a90ff00.yml)
[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0)
[![Donate](https://img.shields.io/badge/$-donate-ff69b4.svg)](https://www.buymeacoffee.com/chunlin)

An image-based CAPTCHA integrated with jigsaw puzzle to prevent from spam bot attack.

## Objective
A team led by [Prof Gao Haichang from Xidian University](https://ieeexplore.ieee.org/author/37403290600) realised that, with the development of 
automated computer vision techniques such as OCR, traditional text-based CAPTHCAs are not considered safe anymore for authentication. 
During the IEEE conference in 2010, [they thus proposed a new way, i.e. using an image based CAPTCHA which involves in solving a jigsaw puzzle](https://ieeexplore.ieee.org/abstract/document/5692499/). 
Their experiments and security analysis further proved that human can complete the jigsaw puzzle CAPTCHA verification quickly and accurately 
which bots rarely can. Hence, jigsaw puzzle CAPTCHA can be a substitution to the text-based CAPTCHA.

In 2019, on [CSDN](https://www.csdn.net/) (Chinese Software Developer Network), a developer [不写BUG的瑾大大](https://blog.csdn.net/a183400826) shared 
[his implementation of jigsaw puzzle captcha in Java](https://blog.csdn.net/a183400826/article/details/90752724). It's a very detailed blog post but 
there is still room for improvement in, for example, documenting the code and naming the variables. Hence, I'd like to take this opportunity to implement 
this jigsaw puzzle CAPTCHA in .NET 5 with C# and Blazor.

## Demo ##

[Demo](https://jpc.chunlinprojects.com/)

<img src="https://gclstorage.blob.core.windows.net/images/Lunar.JigsawPuzzleCaptcha-screenshot01.png" />

<img src="https://gclstorage.blob.core.windows.net/images/Lunar.JigsawPuzzleCaptcha-screenshot02.png" />

## Project Blog Post

[Image Based CAPTCHA using Jigsaw Puzzle on Blazor](https://cuteprogramming.wordpress.com/2021/05/30/image-based-captcha-using-jigsaw-puzzle-on-blazor/)


## License ##

This library is distributed under the GPL-3.0 License found in the [LICENSE](./LICENSE) file.