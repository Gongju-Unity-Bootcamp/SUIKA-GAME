using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CSVReader
{
    public static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))"; // csv ���� ��ȯ ���ڸ� �ڸ��� ���� ���ڿ� ���� ��
    public static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r"; // �ٹٲ� ��ȯ ���ڸ� �ڸ��� ���� ���ڿ� ���� ��
    public static char[] TRIM_CHARS = { '\"' }; // ���� ���Ÿ� ���� ĳ������ �迭 ���� ��

    public static List<Dictionary<string, object>> Read(string file)
        // ��ųʸ��� �̿��Ͽ� ����Ʈ�� �о���̴� ���� �޼ҵ�� ���ڷδ� csv ������ �޴´�
    {
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>(); // ���ڿ� �迭 ���� list�� �����ڸ� �̿��ؼ� ��ųʸ� ����Ʈ�� �����Ѵ�
        TextAsset data = Resources.Load(file) as TextAsset; // ����Ƽ ������ �ؽ�Ʈ ������ ������ �ĺ��ڷ� ���ҽ��� �޾ƿ´�

        string[] lines = Regex.Split(data.text, LINE_SPLIT_RE); // �ٹٲ� ��ȯ ���ڸ� ���� �ڸ��� �� �� lines�� �����Ѵ�

        if (lines.Length <= 1) // ���� ���̰� �ϳ� ���϶��
        {
            return list; // ����Ʈ ��� ��ȯ
        }

        string[] header = Regex.Split(lines[0], SPLIT_RE); // csv ���� ��ȯ ���ڸ� ���� header�� �������� �� header�� �����Ѵ�

        for (int i = 1; i < lines.Length; ++i) // i�� �ε����� �ʱⰪ ������ �� ���� ���� ��ŭ �ݺ��Ѵ�
        {

            string[] values = Regex.Split(lines[i], SPLIT_RE); // csv ���� ��ȯ ���ڸ� ���� values�� �����Ѵ�

            if (values.Length == 0 || values[0] == "") // values�� ���̰� 0 �̰ų� �� ���ڿ��� ��
            {
                continue; // �̹� �ݺ����� �Ʒ� ������ �����ϰ� �ٽ� �ݺ��Ѵ�
            }

            Dictionary<string, object> entry = new Dictionary<string, object>(); // ���ο� ��ųʸ��� �����ڸ� ���� ������ �Ŀ� entry�� �����Ѵ�

            for (int j = 0; j < header.Length && j < values.Length; ++j)
                // j�� �ε��� �ʱⰪ���� ������ �� header��(&& / and ����) values�� �̸��� ������ �ݺ��Ѵ�
            {
                string value = values[j]; // ���ڿ� value�� j�� �ε����� ���ڿ��� �����Ѵ�
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                // value ���ڿ� �հ� ���� ������ ������ ĳ������ �迭�� ���� �� �������� ���� ���ڸ� ���÷��̽� �޼ҵ带 �̿��� �� ���ڿ��� ���ġ�Ѵ�
                object finalvalue = value; // ������Ʈ Ÿ���� finalvalue�� ������ value ���� �����Ѵ�
                int n; // ������ ���� �Է¹��� �� ��ȯ�ϱ� ���� ���� ��
                float f; // �Ǽ��� ���� �Է¹��� �� ��ȯ�ϱ� ���� ���� ��

                if (int.TryParse(value, out n)) // ���������� ����ȯ�� �õ� ������ ��(value ���� ������ ��)
                {
                    finalvalue = n; // finalvalue�� ��ȯ�� ���� ���� �����Ѵ�
                }
                else if (float.TryParse(value, out f)) // �Ǽ������� ����ȯ�� �õ� ������ ��(value ���� ������ ��)
                {
                    finalvalue = f; // finalvalue�� ��ȯ�� �Ǽ� ���� �����Ѵ�
                }

                entry[header[j]] = finalvalue; // �����صξ��� ��ųʸ� entry�� ���� j �ε����� ����ȯ�� ��ģ finalvalue ���� �����Ѵ�
            }

            list.Add(entry); // ��Ʈ�� ��ųʸ��� ����Ʈ�� �߰��Ѵ� add() �޼ҵ�� �ε��� 0���� ���ʷ� �Է��Ѵ�
        }

        return list; // �ݺ����� ��� ��ģ �� ����� ����Ʈ�� ��ȯ�Ѵ�
    }
}
