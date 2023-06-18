export const tools = [
  {
    name: "addTextCard",
    title: "Добавить текстовую карточку",
    icon: `<img src="/js/editor/cards/card-heading.svg" width="17" height="17">`,
    action: function (cardsTool) {
      cardsTool.cards.appendChild(
        cardsTool._createTextCard("Заголовок", "Текст")
      );
    },
  },
  {
    name: "addCard",
    title: "Добавить полную карточку",
    icon: `<img src="/js/editor/cards/icon.svg" width="17" height="17">`,
    action: function (cardsTool) {
      cardsTool.cards.appendChild(
        cardsTool._createCard(
          "Заголовок",
          undefined,
          "Текст",
          "Узнать больше",
          "https://google.com"
        )
      );
    },
  },
  {
    name : "addImageCard",
    title: "Добавить карточку с описанием",
    icon: `<i class="bi bi-card-image"></i>`,
    action: function (cardsTool) {
      cardsTool.cards.appendChild(
        cardsTool._createSimpleImageCard(
          undefined,
          "Текст",
        )
      );
    }
  },
];
