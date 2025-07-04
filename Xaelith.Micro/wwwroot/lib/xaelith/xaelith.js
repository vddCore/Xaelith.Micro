window.xaelith = window.xaelith || {};

window.xaelith.highlightAll = () =>  {
    hljs.highlightAll();
};

window.xaelith.setDocumentTitle = (title) => {
    document.title = title;
};

window.xaelith.enableHorizontalScroll = (sel) => {
    const el = document.querySelector(sel);
    
    if (!el) 
        return;

    el.addEventListener("wheel", (e) => {
        if (e.deltaY === 0) return;
    
        e.preventDefault();
        el.scrollLeft += e.deltaY;
    }, { passive: false });
};

window.xaelith.initScrollGlow = (wrapperSelector) => {
    const wrapper = document.querySelector(wrapperSelector);
    if (!wrapper) return;

    const scrollArea = wrapper.querySelector('.nav-lists');
    if (!scrollArea) return;

    function updateGlow() {
        const isOverflowing = scrollArea.scrollWidth > scrollArea.clientWidth;
        const scrollLeft = scrollArea.scrollLeft;
        const maxScroll = scrollArea.scrollWidth - scrollArea.clientWidth;

        wrapper.classList.toggle('glow-left', scrollLeft > 0);
        wrapper.classList.toggle('glow-right', scrollLeft < maxScroll);
        wrapper.classList.toggle('glow-visible', isOverflowing);
    }

    scrollArea.addEventListener('scroll', updateGlow);
    window.addEventListener('resize', updateGlow);
    updateGlow();
}

window.xaelith.initMobileView = () => {
    let prevScrollpos = window.pageYOffset;

    document.body.addEventListener('scroll', () => {
        if (window.innerWidth <= 836) {
            const mainNav = document.querySelector('#main-nav');
            const mainFooter = document.querySelector('#main-footer');
            
            if (!mainNav || !mainFooter) return;

            const currentScrollPos = document.body.scrollTop;
            const isScrollingUp = prevScrollpos > currentScrollPos;

            mainNav.style.top = isScrollingUp ? "0" : "-100px";
            mainFooter.style.bottom = isScrollingUp ? "0" : "-100px";

            prevScrollpos = currentScrollPos;
        }
    });
}