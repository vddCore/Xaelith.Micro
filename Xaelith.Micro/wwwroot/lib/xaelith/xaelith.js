﻿window.xaelith = window.xaelith || {};

window.xaelith.highlightAll = () =>  {
    hljs.highlightAll();
};

window.xaelith.setDocumentTitle = (title) => {
    document.title = title;
};

window.xaelith.initMobileView = () => {
    let prevScrollpos = window.pageYOffset;

    window.onscroll = () => {
        if (window.innerWidth <= 736) {
            let mainNav = document.querySelector('#main-nav');
            let mainFooter = document.querySelector('#main-footer');

            if (!mainNav || !mainFooter) {
                return;
            }

            let currentScrollPos = window.pageYOffset;

            if (prevScrollpos > currentScrollPos) {
                document.querySelector("#main-nav").style.top = "0";
                document.getElementById("main-footer").style.bottom = "0";
            } else {
                document.getElementById("main-nav").style.top = "-100px";
                document.getElementById("main-footer").style.bottom = "-100px";
            }
            prevScrollpos = currentScrollPos;
        }
    }
}
