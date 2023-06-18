class Image extends React.Component
{
    constructor(props) {
        super(props);
    }
    render(){
        const stretched = this.props.data.stretched && "stretched";
        const withBg = this.props.data.withBackground && "with-background";
        const withBrder = this.props.data.withBorder && "with-border";
        return (
            <div className={`post-block ${stretched} ${withBg} ${withBrder}`}>
                <div className="post-block-content">
                    <img src={this.props.data.file.url} alt={this.props.data.caption} />
                </div>
            </div>
        );
    }
}