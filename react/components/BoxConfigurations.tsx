/* eslint-disable @typescript-eslint/no-explicit-any */
import {
  DataGrid,
  PageContent,
  Heading,
  Input,
  Set,
  useDataGridState,
  Button,
} from '@vtex/admin-ui'
import type { FC } from 'react'
import React, { useState } from 'react'

const BoxConfigurations: FC = () => {
  const [state, setState] = useState<{
    containers: Container[]
  }>({
    containers: [
      {
        height: 6452,
        width: 6345,
        length: 654,
        description: 'hgfdhdf',
        id: '1',
      },
      {
        height: 3357,
        width: 76457654,
        length: 76547645,
        description: 'gfdgsd',
        id: '3',
      },
    ],
  })

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

  const { containers } = state

  const { length, width, height, description } = formState

  const deleteBox = (item: any) => {
    const newContainerList = containers.filter((container) => {
      return container.id !== item.id
    })

    setState({ ...state, containers: newContainerList })
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
            return <Button onClick={() => deleteBox(item)}>Delete</Button>
          },
        },
      },
    ],
    items: containers,
  })

  const addToTable = () => {
    // eslint-disable-next-line vtex/prefer-early-return
    if (Number(length) && Number(width) && Number(height)) {
      const newContainer: Container = {
        height: parseInt(height, 10),
        width: parseInt(width, 10),
        length: parseInt(length, 10),
        description,
        id: (
          parseInt(containers[containers.length - 1]?.id ?? '0', 10) + 1
        ).toString(),
      }

      containers.push(newContainer)

      setFormState({ length: '', width: '', height: '', description: '' })

      setState({ ...state, containers })
    }
  }

  return (
    <PageContent>
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
