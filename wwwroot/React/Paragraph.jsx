class Paragraph extends React.Component
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
        return (
            <div className="post-block"> 
                <div className={ "post-block-content "+pos } >
                    <p dangerouslySetInnerHTML={{ __html: this.props.data.text }}/>
                </div>
            </div>);
    }
}