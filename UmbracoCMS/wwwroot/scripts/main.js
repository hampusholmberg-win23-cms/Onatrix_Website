

const toggleMenu = () => {
    document.querySelector('header').classList.toggle('mobile-toggled');

    // Switching icon on the button
    document.getElementById('btn-mobile-icon').classList.toggle('fa-bars');
    document.getElementById('btn-mobile-icon').classList.toggle('fa-x');
}

const checkScreenSize = () => {

    if (window.innerWidth >= 768) {
        if (document.querySelector('header').classList.contains('mobile-toggled')) {
            toggleMenu()
        }
    }
};

window.addEventListener('resize', checkScreenSize);