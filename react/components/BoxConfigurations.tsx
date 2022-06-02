/* eslint-disable @typescript-eslint/no-explicit-any */
import {
  DataGrid,
  PageContent,
  Heading,
  Input,
  Set,
  useDataGridState,
  Button,
  InputPassword,
  useToast,
} from '@vtex/admin-ui'
import type { FC } from 'react'
import React, { useEffect, useState } from 'react'
import { useQuery, useMutation } from 'react-apollo'
import { useIntl } from 'react-intl'

import AppSettings from '../queries/getAppSettings.gql'
import SaveAppSetting from '../mutations/saveAppSetting.gql'

const BoxConfigurations: FC = () => {
  const { data } = useQuery(AppSettings, {
    ssr: false,
  })

  const [saveAppSetting] = useMutation(SaveAppSetting)

  const [state, setState] = useState<{
    containerList: Container[]
    accessKey: string
  }>({
    containerList: [],
    accessKey: '',
  })

  const { formatMessage } = useIntl()

  useEffect(() => {
    if (!data?.getAppSettings) return

    const { getAppSettings } = data

    setState({ ...getAppSettings })
  }, [data])

  const [formState, setFormState] = useState<{
    length: string
    width: string
    height: string
    description: string
  }>({
    length: '',
    width: '',
    height: '',
    description: '',
  })

  const showToast = useToast()

  const { containerList, accessKey } = state

  const { length, width, height, description } = formState

  const deleteBox = (item: any) => {
    const newContainerList = containerList.filter((container) => {
      return container.id !== item.id
    })

    saveAppSetting({
      variables: {
        appSetting: {
          containerList: newContainerList,
          accessKey,
        },
      },
    }).then((res) => {
      setState({ ...state, containerList: newContainerList })
      let message = formatMessage({
        id: 'admin/packing-optimization.save.success',
      })

      if (!res?.data?.saveAppSetting) {
        message = formatMessage({
          id: 'admin/packing-optimization.save.fail',
        })
      }

      showToast({
        message,
      })
    })
  }

  const table = useDataGridState({
    columns: [
      {
        id: 'id',
        header: 'Box Id',
      },
      {
        id: 'length',
        header: 'Length',
      },
      {
        id: 'width',
        header: 'Width',
      },
      {
        id: 'height',
        header: 'Height',
      },
      {
        id: 'description',
        header: 'Box Description',
      },
      {
        id: 'delete',
        header: 'Delete',
        resolver: {
          type: 'plain',
          render: ({ item }) => {
            return (
              <Button
                variant="danger-secondary"
                onClick={() => deleteBox(item)}
              >
                Delete
              </Button>
            )
          },
        },
      },
    ],
    items: containerList,
  })

  const addToTable = () => {
    // eslint-disable-next-line vtex/prefer-early-return
    if (Number(length) && Number(width) && Number(height)) {
      const newContainer: Container = {
        height: parseInt(height, 10),
        width: parseInt(width, 10),
        length: parseInt(length, 10),
        description,
        id: (containerList[containerList.length - 1]?.id ?? 0) + 1,
      }

      containerList.push(newContainer)

      saveAppSetting({
        variables: {
          appSetting: {
            containerList,
            accessKey,
          },
        },
      }).then((res) => {
        setFormState({ length: '', width: '', height: '', description: '' })

        setState({ ...state, containerList })
        let message = formatMessage({
          id: 'admin/packing-optimization.save.success',
        })

        if (!res?.data?.saveAppSetting) {
          message = formatMessage({
            id: 'admin/packing-optimization.save.fail',
          })
        }

        showToast({
          message,
        })
      })
    }
  }

  const saveKey = () => {
    saveAppSetting({
      variables: {
        appSetting: {
          containerList,
          accessKey,
        },
      },
    }).then((res) => {
      let message = formatMessage({
        id: 'admin/packing-optimization.save.success',
      })

      if (!res?.data?.saveAppSetting) {
        message = formatMessage({
          id: 'admin/packing-optimization.save.fail',
        })
      }

      showToast({
        message,
      })
    })
  }

  return (
    <PageContent>
      <Set spacing={3}>
        <InputPassword
          id="accessKey"
          label="Access Key"
          value={accessKey}
          onChange={(e) => setState({ ...state, accessKey: e.target.value })}
        />
        <Button onClick={() => saveKey()}>Save</Button>
      </Set>
      <Heading className="pt6">Your Boxes</Heading>
      <Set spacing={3}>
        <Input
          id="length"
          label="Length"
          value={length}
          onChange={(e) => {
            setFormState({
              ...formState,
              length: e.target.value,
            })
          }}
        />
        <Input
          id="width"
          label="Width"
          value={width}
          onChange={(e) => {
            setFormState({
              ...formState,
              width: e.target.value,
            })
          }}
        />
        <Input
          id="height"
          label="Height"
          value={height}
          onChange={(e) => {
            setFormState({
              ...formState,
              height: e.target.value,
            })
          }}
        />
        <Input
          id="description"
          label="Box Description"
          value={description}
          onChange={(e) => {
            setFormState({
              ...formState,
              description: e.target.value,
            })
          }}
        />
        <Button onClick={() => addToTable()}>Add To Table</Button>
      </Set>
      <DataGrid state={table} />
    </PageContent>
  )
}

export default BoxConfigurations
