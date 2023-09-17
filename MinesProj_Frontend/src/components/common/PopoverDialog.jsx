import React, { useRef } from 'react'
import {
  ChakraProvider,
  extendTheme,
  HStack,
  VStack,
  Box,
  Input,
  Select,
  Popover,
  PopoverTrigger,
  PopoverContent,
  PopoverBody,
  PopoverFooter,
  PopoverArrow,
  PopoverCloseButton,
  FormLabel,
  Radio,
  RadioGroup,
  createMultiStyleConfigHelpers,
} from '@chakra-ui/react'
import { InfoIcon } from '@chakra-ui/icons'
import { selectAnatomy } from '@chakra-ui/anatomy'

const { definePartsStyle, defineMultiStyleConfig } =
  createMultiStyleConfigHelpers(selectAnatomy.keys)

const baseStyle = definePartsStyle({
  field: {
    color: 'white',
    ps: '18%',
  },
  icon: {
    color: 'blue.400',
    right: 0
  },
})
const selectTheme = defineMultiStyleConfig({ baseStyle })

let filterApplied = true

const PopoverDialog = ({
  id,
  label,
  inputKind,
  filter,
  setFilter,
  upperBoundFilter,
  setUpperBoundFilter,
  setApplyFilter,
  dropdownDictionary,
  selectedProvinceId,
  countyToProvinceIdDictionary,
}) => {
  const optionStyles = {backgroundColor: '#2a4365'}
  let input = <></>
  const initialInputRef = useRef(null)
  const secondInputRef = useRef(null)
  const closeBtnRef = useRef(null)
  const closeBtnPosition =
    inputKind === 'radio' ? { right: 0.5, top: 0.5 } : { right: 1, top: 1 }

  switch (inputKind) {
    case 'input':
      input = (
        <>
          <Input
            ref={initialInputRef}
            id={id}
            size="sm"
            placeholder="جستجو..."
            value={filter ? filter : ''}
            onChange={(e) => {
              setFilter(e.target.value)
              filterApplied = false
            }}
            onKeyDown={(e) => {
              if (checkAndHandleEnter(e)) closeBtnRef.current.click()
            }}
            onBlur={() => handleApplying()}
          />
        </>
      )
      break
    case 'select':
      input = (
        <>
          <Select
            ref={initialInputRef}
            id={id}
            size="sm"
            me={filter ? dropdownDictionary[filter].length <=10 ? 4 : 7 : 6}
            value={filter ? filter : -1}
            onChange={(e) => {
              const selectedOptionValue = e.target.value
              if (selectedOptionValue !== '-1') setFilter(selectedOptionValue)
              else setFilter()
            }}
          >
            <option value={-1} style={optionStyles}>{`انتخاب ${label}`}</option>
            {selectedProvinceId && label === 'شهرستان'
              ? Object.keys(dropdownDictionary).map((id) =>
                  countyToProvinceIdDictionary[id] ===
                  parseInt(selectedProvinceId) ? (
                    <option key={id} value={id} style={optionStyles}>
                      {dropdownDictionary[id]}
                    </option>
                  ) : null
                )
              : Object.keys(dropdownDictionary).map((id) => (
                  <option key={id} value={id} style={optionStyles}>
                    {dropdownDictionary[id]}
                  </option>
                ))}
          </Select>
        </>
      )
      break
    case 'interval':
      input = (
        <HStack spacing="6px">
          <span>بین</span>
          <Input
            ref={initialInputRef}
            size="sm"
            placeholder="ابتدا"
            value={filter ? filter : ''}
            onChange={(e) => {
              const inputValue = e.target.value
              setFilter(inputValue ? inputValue : undefined)
              filterApplied = false
            }}
            onKeyDown={(e) => {
              if (e.key === 'Enter') secondInputRef.current.focus()
            }}
            onBlur={() => secondInputRef.current.focus()}
          />
          <span>و</span>
          <Input
            ref={secondInputRef}
            size="sm"
            placeholder="انتها"
            value={upperBoundFilter ? upperBoundFilter : ''}
            onChange={(e) => {
              const inputValue = e.target.value
              setUpperBoundFilter(inputValue ? inputValue : undefined)
              filterApplied = false
            }}
            onKeyDown={(e) => {
              if (checkAndHandleEnter(e)) closeBtnRef.current.click()
            }}
            onBlur={() => handleApplying()}
          />
        </HStack>
      )
      break
    case 'radio':
      input = (
        <RadioGroup
          defaultValue="undefined"
          onChange={(radioGroupValue) => {
            setFilter(
              radioGroupValue === 'true'
                ? true
                : radioGroupValue === 'false'
                ? false
                : undefined
            )
          }}
          display={'flex'}
          flexDir={'column'}
          justifyContent={'start'}
        >
          <Radio value={'undefined'} size={'sm'} mb={'2%'}>
            همه
          </Radio>
          <Radio value={'true'} size={'sm'} mb={'2%'}>
            دارای تعهد
          </Radio>
          <Radio value={'false'} size={'sm'} mb={'2%'}>
            بدون تعهد
          </Radio>
        </RadioGroup>
      )
      break
  }

  const checkAndHandleEnter = (e) => {
    const enterWasPressed = e.key === 'Enter'
    if (enterWasPressed) handleApplying()
    return enterWasPressed
  }

  const handleApplying = () => {
    if (!filterApplied) {
      setApplyFilter(true)
      filterApplied = true
    }
  }

  return (
    <ChakraProvider
      theme={extendTheme({
        styles: {
          global: () => ({
            body: {
              bg: '',
            },
          }),
        },
        components: { Select: selectTheme },
      })}
    >
      <Popover
        initialFocusRef={initialInputRef}
        placement="bottom"
        closeOnBlur={true}
      >
        <PopoverTrigger>
          <span>{label}</span>
        </PopoverTrigger>
        <PopoverContent
          color="white"
          bg="blue.800"
          borderColor="blue.800"
          width="100%"
        >
          <PopoverArrow bg="blue.800" />
          <PopoverCloseButton
            size="sm"
            ref={closeBtnRef}
            {...closeBtnPosition}
          />
          <PopoverBody
            pb={'7%'}
            pt={
              inputKind !== 'interval'
                ? label === 'شماره معدن'
                  ? '2%'
                  : '4%'
                : '1.5%'
            }
          >
            <FormLabel
              htmlFor={id}
              fontSize="sm"
              color={'blue.200'}
              width={inputKind === 'radio' ? '102%' : 'inherit'}
              ps={inputKind === 'radio' ? '22%' : '13%'}
              pt={0}
            >
              {label}
            </FormLabel>
            {input}
          </PopoverBody>
          {inputKind !== 'radio' && inputKind !== 'select' ? (
            <PopoverFooter
              border="0"
              display="flex"
              justifyContent="center"
              alignItems="center"
              cursor={'default'}
            >
              <VStack spacing={1}>
                <Box fontSize="x-small">
                  <span>برای اعمال فیلتر Enter را فشار دهید</span>
                </Box>

                {inputKind === 'interval' || label === 'شماره معدن' ? (
                  <Box fontSize="x-small" color={'skyblue'}>
                    <InfoIcon fontSize="md" pe={1} />
                    {inputKind === 'interval' ? (
                      <span>
                        برای محدود نکردن بازه از یک طرف، فیلد مربوط به آن را
                        خالی بگذارید
                      </span>
                    ) : (
                      <span>
                        معدن هایی که شماره ای شامل این فیلتر داشته باشند نمایش
                        داده می شود
                      </span>
                    )}
                  </Box>
                ) : null}
              </VStack>
            </PopoverFooter>
          ) : null}
        </PopoverContent>
      </Popover>
    </ChakraProvider>
  )
}

export default PopoverDialog
