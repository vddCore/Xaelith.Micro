function createBars(containerSelector) {
    const container = document.querySelector(containerSelector);

    if (!container)
        return;

    container.innerHTML = "";

    let computedStyle = window.getComputedStyle(document.body);
    
    const barWidth = parseInt(
        computedStyle
            .getPropertyValue("--bar-width")
            .replace("px", "")
    ) || 12;

    const spacing = parseInt(
        computedStyle
            .getPropertyValue("--bar-gap")
            .replace("px", "")
    ) || 4;

    const barFullHeight = parseInt(
        computedStyle
            .getPropertyValue("--bar-full-height")
            .replace("px", "")
    ) || 4;
    
    const totalWidth = container.offsetWidth;
    const count = Math.floor(totalWidth / (barWidth + spacing));

    for (let i = 0; i < count; i++) {
        const bar = document.createElement("div");
        bar.classList.add("bar");

        const maxHeight = 10 + Math.random() * barFullHeight;
        const perBarWidth = 2 + Math.random() * 12;
        bar.style.setProperty("--bar-full-height", `${maxHeight}px`);
        bar.style.setProperty("--bar-width", `${perBarWidth}px`);

        bar.style.animationDelay = `${Math.random(3, 12)}s`;
        container.appendChild(bar);
    }

    requestAnimationFrame(() => {
        container.querySelectorAll('.bar').forEach(bar => {
            bar.classList.add('visible');
        });
    });
}

function setupWaveBars() {
    createBars(".wave-row.top");
    createBars(".wave-row.bottom");
}


let resizeTimeout;
window.addEventListener("resize", () => {
    clearTimeout(resizeTimeout);
    resizeTimeout = setTimeout(setupWaveBars, 100);
});

window.addEventListener("DOMContentLoaded", setupWaveBars);