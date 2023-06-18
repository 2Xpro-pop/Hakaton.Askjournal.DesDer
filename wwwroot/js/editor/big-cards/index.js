import { tools } from "./tools.js";

export class BigCardsTool {
    cards;
    static get toolbox() {
        return {
            title: "Большие карты",
            icon: '<img src="/js/editor/quote/icon.svg" width="17" height="17">',
        };
    }

    constructor({ data, config, api }) {
        this.data = {
            cards: data.cards || [
                { header: "Заголовок", text: "Текст" },
            ],
        };
        this.config = config;
        this.api = api;
        this.cards = document.createElement("div");
        this.cards.className = "row align-items-stretch d-flex justify-content-around mt-2 mb-5 mx-2";
    }

    renderSettings() {
        const wrapper = document.createElement("div");

        tools.forEach((tune) => {
            let button = document.createElement("div");

            button.classList.add("cdx-settings-button");
            button.innerHTML = tune.icon;
            button.onclick = () => tune.action(this);
            wrapper.appendChild(button);
        });

        return wrapper;
    }

    render() {
        for (let i = 0; i < this.data.cards.length; i++) {
            let card = this.data.cards[i];

            this.cards.appendChild(this._createCard(card.header, card.text));
        }

        return this.cards;
    }


    _createCard(header, text) {
        var colDiv = document.createElement("div");
        colDiv.className = "col-sm-12 col-md-6 px-sm-1 px-md-4 mt-3";

        var cardDiv = document.createElement("div");
        cardDiv.className = "tlp-card p-4 text-center text-break";

        var heading3 = document.createElement("h3");
        heading3.className = "tlp-inter-600";
        heading3.textContent = header;
        heading3.setAttribute("contenteditable", "true");

        var paragraph = document.createElement("p");
        paragraph.className = "p-2";
        paragraph.textContent = text;
        paragraph.setAttribute("contenteditable", "true");

        cardDiv.appendChild(heading3);
        cardDiv.appendChild(paragraph);

        colDiv.appendChild(cardDiv);

        return colDiv;
    }

    save() {
        let data = { cards: [] };
        this.cards.childNodes.forEach((card) => {
            data.cards.push(
                {
                    header: card.querySelector("h3").innerText,
                    text: card.querySelector("p").innerText
                }
            )
        });

        return data;
    }
}