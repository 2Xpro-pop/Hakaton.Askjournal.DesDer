window.addEventListener('scroll',()=>{
    let scrollDistance = window.scrollY
    if(scrollDistance > 240 ){
        document.getElementById("main-nav").style.background = "black";
        document.getElementById("main-nav").style.transition = "all 0.4s";
        document.getElementById("nav-text").style.color = "white";
        document.getElementById("menubar").classList.add("navbar-dark");
        let elements = document.getElementsByClassName('nav-link');
        for (i = 0; i < elements.length; i++) {
            elements[i].style.color = 'white';
        }
    }else{
        document.getElementById("nav-text").style.color = header_primary_color;
        let elements = document.getElementsByClassName('nav-link');
        document.getElementById("menubar").classList.remove("navbar-dark");
        for (i = 0; i < elements.length; i++) {
            elements[i].style.color = header_primary_color;
        }
        document.getElementById("main-nav").style.background = "none";
    }
})
