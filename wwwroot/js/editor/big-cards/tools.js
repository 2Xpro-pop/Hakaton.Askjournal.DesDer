export const tools = [
    {
        name: "addTextCard",
        title: "Добавить тцитату",
        icon: `<img src="/js/editor/quote/plus.svg" width="17" height="17">`,
        action: function (cardsTool) {
            cardsTool.cards.appendChild(
                cardsTool._createCard("Заголовок", "Текст")
            );
        },
    }
]