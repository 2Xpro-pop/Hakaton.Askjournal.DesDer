class Carousel extends React.Component
{
    constructor(props) {
        super(props);
        this.state = {data : props.data };
    }
    
    render(){
        let pos = "text-left"
        switch (this.props.tunes?.alignment?.alignment) {
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
        let indicators = this.props.data.items.map((item, index) => {
            let active = index == 0 ? "active" : "";
            return <button type="button" data-bs-slide-to={index} data-bs-target={"#id"+this.props.data.id} className={active} />;
        })
        let items = this.props.data.items.map((item, index) => {
            let active = index == 0 ? "active" : "";
            return (
                <div className={"carousel-item "+ active}>
                    <img src={item.url} alt={item.caption.header} className="d-block"/>
                    <div className="carousel-caption">
                        <h5 dangerouslySetInnerHTML={{__html: item.caption.header}}/>
                        <p dangerouslySetInnerHTML={{__html: item.caption.text}}/>
                    </div>
                    { item.caption.button && 
                        <div className="side-btn">
                            <a href={item.caption.button.linkUrl} className="btn btn-dark" dangerouslySetInnerHTML={{__html: item.caption.button.text}}/>
                        </div>
                    }
                </div>
            );
        });
        return (
            <div className={"post-block " + (this.props.data.stretched && "stretched")}>
                <div className={ "post-block-content "+pos } >
                    <div id={"id"+this.props.data.id} className="carousel slide" data-bs-ride="carousel">
                        <div className="carousel-indicators">
                            {indicators}
                        </div>
                        <div className="carousel-inner">
                            {items}
                        </div>
                        <button type="button" className="carousel-control-prev" data-bs-slide="prev" data-bs-target={"#id"+this.props.data.id} ><span
                            className="carousel-control-prev-icon"></span></button>
                    <button type="button" className="carousel-control-next" data-bs-slide="next" data-bs-target={"#id"+this.props.data.id} ><span
                            className="carousel-control-next-icon"></span></button>
                    </div>
                    
                </div>
            </div>
        )
    }
}