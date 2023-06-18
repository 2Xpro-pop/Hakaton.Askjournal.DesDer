class Cards extends React.Component
{
    constructor(props) {
        super(props);
    }

    render(){
        let pos = "text-left"
        switch (this.props.tunes.alignment.alignment) {
            case "left":
                pos = "text-left";
                break;

            case "center":
                pos = "text-center";
                break;

            case "right":
                pos = "text-right";
                break;
        
            default:
                break;
        }

        let cards = this.props.data.cards.map(card => {
            if(card.type == "text")
            {
                return (
                    <div className="info-card">
                        <h3 dangerouslySetInnerHTML={{__html: card.header}}/>
                        <p dangerouslySetInnerHTML={{__html: card.text}}/>
                    </div>
                );
            }
            if(card.type == "full")
            {
                return (
                    <div className="info-card">
                        <h3 dangerouslySetInnerHTML={{__html: card.header}}/>
                        <img src={card.imageUrl} alt={card.header}/>
                        <a href={card.linkUrl} className='btn btn-dark' dangerouslySetInnerHTML={{__html: card.linkText}}/>
                        <p dangerouslySetInnerHTML={{__html: card.text}}/>
                    </div>
                );
            }
        });
        return (
            <div className="post-block"> 
                <div className={ "post-block-content "+pos } >
                    <div className="row">
                        {cards}
                    </div>
                </div>
            </div>
        );
    }
}