(function() {
  function highlightAll() {
    hljs.highlightAll();
  }

  window.xaelith = window.xaelith || {};

  window.xaelith.onLoad = {
    highlightAll: highlightAll
  };
  
  window.xaelith.filterHeaderAnimation = function() {
    var h1 = document
      .getElementsByClassName("filter-header")[0]
      .getElementsByTagName("h1")[0];
                 
    var h2 = document
      .getElementsByClassName("filter-header")[0]
      .getElementsByTagName("h2")[0];
                     
    if (h1) {
      window.setTimeout(function() {
        h1.style.width = "100%";
      }, 75);
      
      window.setTimeout(function() {
        h1.style.overflow = "auto";
        h1.style.textWrap = "wrap";
      }, 1000);
    }

    if (h2) {
      window.setTimeout(function() {
        h2.style.width = "100%";
      }, 150);
      
      window.setTimeout(function() {
        h2.style.overflow = "auto";
        h2.style.textWrap = "wrap";
      }, 1000);
    }
  }
})();

let prevScrollpos = window.pageYOffset;

window.onscroll = function() {
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