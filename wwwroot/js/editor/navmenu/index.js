export class NavmenuTool {

    static get toolbox() {
        return {
            title: "Навигационное меню",
            icon: '<img src="/js/editor/navmenu/icon.svg" width="17" height="17">',
        };
    }

    render() {
        function createNavItem(itemData) {
            const li = document.createElement('li');
            li.classList.add('nav-item');
            li.style.width = 'min-content';

            const a = document.createElement('a');
            a.classList.add('nav-link', 'd-flex', 'flex-column', 'align-items-center', 'justify-content-center');
            a.setAttribute('aria-current', 'page');
            a.href = itemData.href;

            const img = document.createElement('img');
            img.src = itemData.iconSrc;
            img.alt = itemData.iconAlt;
            img.classList.add('img-fluid');
            img.style.width = '24px';
            img.style.height = '25px';

            const p = document.createElement('p');
            p.classList.add('mb-0');
            p.textContent = itemData.text;

            a.appendChild(img);
            a.appendChild(p);
            li.appendChild(a);

            return li;
        }

        const container = document.createElement('div');
        container.classList.add('container', 'mt-5');

        const nav = document.createElement('nav');

        const bottomNavbar = document.createElement('div');
        bottomNavbar.classList.add('w-100', 'border', 'border-1');
        bottomNavbar.id = 'bottomNavbar'
        bottomNavbar.style.borderRadius = '15px';

        const ul = document.createElement('ul');
        ul.classList.add('navbar-nav', 'd-flex', 'justify-content-around', 'flex-row', 'w-100', 'py-3', 'tlp-bootombar');
        ul.style.fontSize = '12px';
        ul.style.textAlign = 'center';

        const listItemsData = [
            {
                iconSrc: '/imgs/kirka.png',
                iconAlt: 'kirka',
                text: 'Добывающая отрасль',
                href: '#'
            },
            {
                iconSrc: '/imgs/post.png',
                iconAlt: 'post',
                text: 'Посты',
                href: '#'
            },
            {
                iconSrc: '/imgs/vlast.png',
                iconAlt: 'vlast',
                text: 'Власть',
                href: '#'
            },
            {
                iconSrc: '/imgs/pravovaya.png',
                iconAlt: 'pravovaya',
                text: 'Правовая регуляция',
                href: '#'
            },
            {
                iconSrc: '/imgs/instructions.png',
                iconAlt: 'instructions',
                text: 'Инструкции',
                href: '#'
            }
        ];

        listItemsData.forEach(itemData => {
            const li = createNavItem(itemData);
            ul.appendChild(li);
        });

        bottomNavbar.appendChild(ul);
        nav.appendChild(bottomNavbar);
        container.appendChild(nav);

        return container;
    }

    save() {
        return { name: "navmenu" };
    }

}