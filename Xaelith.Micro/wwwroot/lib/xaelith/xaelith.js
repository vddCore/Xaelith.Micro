(function () {
    window.xaelith = window.xaelith || {};

    window.xaelith = {
        highlightAll: () => {
            hljs.highlightAll();
        },

        setDocumentTitle: (title) => {
            document.title = title;
        }
    }

    let prevScrollpos = window.pageYOffset;

    window.onscroll = () => {
        if (window.innerWidth <= 736) {
            let currentScrollPos = window.pageYOffset;

            if (prevScrollpos > currentScrollPos) {
                document.getElementById("main-nav").style.top = "0";
                document.getElementById("main-footer").style.bottom = "0";
            } else {
                document.getElementById("main-nav").style.top = "-100px";
                document.getElementById("main-footer").style.bottom = "-100px";
            }
            prevScrollpos = currentScrollPos;
        }
    }
})();