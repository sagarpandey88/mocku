import * as React from "react";

export interface IEndPointHeaderProps {
  ExistingHeaders: IEndPointHeader[];
  getHeadersCallback: (headers:IEndPointHeader[]) => void ;
}

export interface IEndPointHeader {
  key: string;
  value: string;
  order: number;
}

export const EndPointHeaders = (props: IEndPointHeaderProps) => {
  const [headers, setHeaders] = React.useState<IEndPointHeader[]>([]);

  const onKeyValueChange = (
    e: React.FormEvent<HTMLInputElement>,
    item: IEndPointHeader,
    itemType: string
  ) => {
    const tmpHeaders = [...headers];

    itemType == "key"
      ? (tmpHeaders[item.order].key = e.currentTarget.value)
      : (tmpHeaders[item.order].value = e.currentTarget.value);

    setHeaders(tmpHeaders);

    props.getHeadersCallback(tmpHeaders);
  };

  const addRow = (e: React.FormEvent<HTMLInputElement>) => {
    const tmpHeaders = [...headers];
    tmpHeaders.push({ key: "", value: "", order: tmpHeaders.length });
    setHeaders(tmpHeaders);
  };

  return (
    <>
      <div>
        <table>
          {headers?.map((x) => {
            return (
              <>
                <tr key={x.order}>
                  <td>
                    <input
                      type="input"
                      value={x.key}
                      onChange={(e) => {
                        onKeyValueChange(e, x, "key");
                      }}
                    ></input>
                  </td>
                  <td>
                    <input
                      type="input"
                      value={x.value}
                      onChange={(e) => {
                        onKeyValueChange(e, x, "value");
                      }}
                    ></input>
                  </td>
                </tr>
              </>
            );
          })}
        </table>
        <input type="button" value="+Add" onClick={addRow}></input>
      </div>
    </>
  );
};
